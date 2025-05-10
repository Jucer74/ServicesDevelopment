using UserManagement.Domain.Entities;
using UserManagement.Application.Interfaces;
using UserManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace UserManagement.Application.Services;

public class PersonService : IPersonService
{
    private readonly ApplicationDbContext _context;

    public PersonService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Person>> GetAllAsync()
    {
        return await _context.Persons.ToListAsync();
    }

    public async Task<Person> GetByIdAsync(int id)
    {
        return await _context.Persons.FindAsync(id);
    }

    public async Task<Person> AddAsync(Person person)
    {
        _context.Persons.Add(person);
        await _context.SaveChangesAsync();
        return person;
    }

    public async Task UpdateAsync(Person person)
    {
        _context.Entry(person).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var person = await _context.Persons.FindAsync(id);
        if (person != null)
        {
            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
        }
    }
}