<<<<<<< HEAD:Practices/02.WebAPI/TeamsApi/Services/ITeamMemberService.cs
﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TeamsApi.Models;
=======
﻿using TeamsApi.Models;

namespace TeamsApi.Services;
>>>>>>> main:Practices/03.WebAPI/TeamsApi/Services/ITeamMemberService.cs

public interface ITeamMemberService
{
    Task<TeamMember> CreateTeamMember(TeamMember teamMember);
<<<<<<< HEAD:Practices/02.WebAPI/TeamsApi/Services/ITeamMemberService.cs
    Task DeleteTeamMember(int id);
    Task<List<TeamMember>> GetAllTeamMembers();
    Task<TeamMember> GetTeamMemberById(int id);
    Task<TeamMember> UpdateTeamMember(TeamMember teamMember);
}

=======

    Task DeleteTeamMember(int id);

    Task<List<TeamMember>> GetAllTeamMembers();

    Task<TeamMember> GetTeamMemberById(int id);

    Task<TeamMember> UpdateTeamMember(int id, TeamMember teamMember);

}
>>>>>>> main:Practices/03.WebAPI/TeamsApi/Services/ITeamMemberService.cs
