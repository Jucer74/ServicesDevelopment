using Members.Domain.Entities;
using Members.Domain.Interfaces;
using Members.Infrastructure.Common;
using Members.Infrastructure.Context;

namespace Members.Infrastructure.Repositories;

public class MemberRepository : Repository<Member>, IMemberRepository
{
    public MemberRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}