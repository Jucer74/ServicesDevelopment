using PizzeriaApi.Models;

namespace PizzeriaApi.Services;

public interface IPizzeriaService
{
    Task<Pizzeria> CreatePizzeria(Pizzeria Pizzeria);
    Task DeletePizzeria(int id);
    Task<List<Pizzeria>> GetAllPizzerias();
    Task<Pizzeria> GetPizzeriaById(int id);
    Task<Pizzeria> UpdatePizzeria(int id, Pizzeria Pizzeria);
    Task<List<PizzeriaCategoria>> GetPizzeriaCategoriaByPizzeriaId(int id);
}
