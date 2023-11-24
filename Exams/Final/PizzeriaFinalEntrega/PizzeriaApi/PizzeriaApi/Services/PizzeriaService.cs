using PizzeriaApi.Context;
using PizzeriaApi.Exceptions;
using PizzeriaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace PizzeriaApi.Services;

public class PizzeriaService : IPizzeriaService
{
    private readonly AppDbContext _appDbContext;

    public PizzeriaService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
        
    public async Task<Pizzeria> CreatePizzeria(Pizzeria Pizzeria)
    {
        _appDbContext.Set<Pizzeria>().Add(Pizzeria);
        await _appDbContext.SaveChangesAsync();
        return Pizzeria;
    }

    public async Task DeletePizzeria(int id)
    {
        var original = await _appDbContext.Set<Pizzeria>().FindAsync(id);

        if (original is null)
        {
            throw new NotFoundException($"Pizzeria with Id={id} Not Found");
        }

        _appDbContext.Set<Pizzeria>().Remove(original);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<List<Pizzeria>> GetAllPizzerias()
    {
        return await _appDbContext.Set<Pizzeria>().ToListAsync<Pizzeria>();
    }

    public async Task<Pizzeria> GetPizzeriaById(int id)
    {
        var Pizzeria = await _appDbContext.Set<Pizzeria>().FindAsync(id);
        if (Pizzeria is null)
        {
            throw new NotFoundException($"Pizzeria with Id={id} Not Found");
        }
        return Pizzeria!;
    }

    public async Task<List<PizzeriaCategoria>> GetPizzeriaCategoriaByPizzeriaId(int id)
    {
        var PizzeriaCategoria = await _appDbContext.Pizzas
                                .Include(m => m.Categoria)
                                .Where(t => t.Id == id)
                                .FirstOrDefaultAsync();
        return PizzeriaCategoria!.Categoria;
    }

    public async Task<Pizzeria> UpdatePizzeria(int id, Pizzeria Pizzeria)
    {
        if (id != Pizzeria.Id)
        {
            throw new BadRequestException($"Id [{id}] is different to Pizzeria.Id [{Pizzeria.Id}]");
        }

        var original = await _appDbContext.Set<Pizzeria>().FindAsync(id);

        if (original is null)
        {
            throw new NotFoundException($"Pizzeria with Id={id} Not Found");
        }

        _appDbContext.Entry(original).CurrentValues.SetValues(Pizzeria!);
        await _appDbContext.SaveChangesAsync();

        return Pizzeria!;
    }
}
