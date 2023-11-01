using AutoMapper;
using DbFirst.ActionResponses.Status;
using DbFirst.ActionResponses.TeamResponse;
using DbFirst.Interfaces;
using DbFirst.Models;
using DbFirst.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DbFirst.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly LeagueDatabaseContext _leagueDatabaseContext;
        private readonly IMapper _iMapper;

        public TeamRepository(LeagueDatabaseContext leagueDatabaseContext, IMapper iMapper)
        {
            _leagueDatabaseContext = leagueDatabaseContext;
            _iMapper = iMapper;
        }

        public Task<ActionResult<TeamDTO>> CreateTeam(TeamDTO team)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<TeamDTO>> DeleteTeam(int IdTeam)
        {
            throw new NotImplementedException();
        }

        public Task<Team> GetTeamByID(int IdTeam)
        {
            throw new NotImplementedException();
        }

        public Task<List<Team>> GetTeams(int page = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<TeamDTO>> UpdateTeam(int IdTeam, TeamDTO team)
        {
            throw new NotImplementedException();
        }
    }
}
