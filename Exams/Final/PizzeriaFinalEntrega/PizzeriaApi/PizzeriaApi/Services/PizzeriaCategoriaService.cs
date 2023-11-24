using PizzeriaApi.Context;
using PizzeriaApi.Exceptions;
using PizzeriaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace PizzeriaApi.Services;

public class PizzeriaCategoriaService : IPizzeriaCategoriaService
{
    private readonly AppDbContext _appDbContext;

    public PizzeriaCategoriaService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<PizzeriaCategoria> CreatePizzeriaCategoria(PizzeriaCategoria PizzeriaCategoria)
    {
        _appDbContext.Set<PizzeriaCategoria>().Add(PizzeriaCategoria);
        await _appDbContext.SaveChangesAsync();
        return PizzeriaCategoria;
    }

    public async Task DeletePizzeriaCategoria(int id)
    {
        var original = await _appDbContext.Set<PizzeriaCategoria>().FindAsync(id);

        if (original is null)
        {
            throw new NotFoundException($"Pizzeria Categoria with Id={id} Not Found");
        }

        _appDbContext.Set<PizzeriaCategoria>().Remove(original);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<List<PizzeriaCategoria>> GetAllPizzeriaCategorias()
    {
        return await _appDbContext.Set<PizzeriaCategoria>().ToListAsync<PizzeriaCategoria>();
    }

    public async Task<PizzeriaCategoria> GetPizzeriaCategoriaById(int id)
    {
        var PizzeriaCategoria = await _appDbContext.Set<PizzeriaCategoria>().FindAsync(id);
        if (PizzeriaCategoria is null)
        {
            throw new NotFoundException($"Pizzeria Categoria with Id={id} Not Found");
        }

        return PizzeriaCategoria!;
    }

    public async Task<PizzeriaCategoria> UpdatePizzeriaCategoria(int id, PizzeriaCategoria PizzeriaCategoria)
    {
        if (id != PizzeriaCategoria.Id)
        {
            throw new BadRequestException($"Id [{id}] is different to PizzeriaCategoria.Id [{PizzeriaCategoria.Id}]");
        }

        var original = await _appDbContext.Set<PizzeriaCategoria>().FindAsync(id);

        if (original is null)
        {
            throw new NotFoundException($"Pizzeria Categoria with Id={id} Not Found");
        }

        _appDbContext.Entry(original).CurrentValues.SetValues(PizzeriaCategoria!);
        await _appDbContext.SaveChangesAsync();

        return PizzeriaCategoria!;
    }
}
