using PizzeriaApi.Models;

namespace PizzeriaApi.Services;

public interface IPizzeriaCategoriaService
{
    Task<PizzeriaCategoria> CreatePizzeriaCategoria(PizzeriaCategoria PizzeriaCategoria);
    Task DeletePizzeriaCategoria(int id);
    Task<List<PizzeriaCategoria>> GetAllPizzeriaCategorias();
    Task<PizzeriaCategoria> GetPizzeriaCategoriaById(int id);
    Task<PizzeriaCategoria> UpdatePizzeriaCategoria(int id, PizzeriaCategoria PizzeriaCategoria);
}
