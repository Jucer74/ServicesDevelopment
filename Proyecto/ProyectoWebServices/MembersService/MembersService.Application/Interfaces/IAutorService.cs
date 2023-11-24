using MembersService.Domain.Entities;
using System.Linq.Expressions;
using System;

namespace MembersService.Application.Interfaces;

public interface IAutorService
{
    public Task<Autor> AddAsync(Autor entity);

    public Task<IEnumerable<Autor>> GetAllAsync();
    public Task<Autor> GetByIdAsync(int id);
    public Task<IEnumerable<Autor>> FindAsync(Expression<Func<Autor, bool>> predicate);
    public Task<Autor> UpdateAsync(int id, Autor entity);

    public Task RemoveAsync(int id);

    public Task RemoveMembersByTeamId(Expression<Func<Autor, bool>> predicate, int id);
}
