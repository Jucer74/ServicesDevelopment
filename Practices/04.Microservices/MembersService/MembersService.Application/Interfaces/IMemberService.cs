using MembersService.Domain.Entities;
using System.Linq.Expressions;
using System;

namespace MembersService.Application.Interfaces;

public interface IMemberService
{
    public Task<Member> AddAsync(Member entity);

    public Task<IEnumerable<Member>> GetAllAsync();
    public Task<Member> GetByIdAsync(int id);
    public Task<IEnumerable<Member>> FindAsync(Expression<Func<Member, bool>> predicate);
    public Task<Member> UpdateAsync(int id, Member entity);

    public Task RemoveAsync(int id);

    public Task RemoveMembersByTeamId(Expression<Func<Member, bool>> predicate, int id);
}
