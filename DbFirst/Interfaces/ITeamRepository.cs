using Azure;
using DbFirst.ActionResponses.TeamResponse;
using DbFirst.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DbFirst.Interfaces
{
    public interface ITeamRepository
    {
        Task<List<Team>> GetTeams(int page = 1, int pageSize = 10);
        Task<Team> GetTeamByID(int IdTeam);
        Task<ActionResult<TeamDTO>> CreateTeam(TeamDTO team);
        Task<ActionResult<TeamDTO>> DeleteTeam(int IdTeam);
        Task<ActionResult<TeamDTO>> UpdateTeam(int IdTeam, TeamDTO team);
    }

}
