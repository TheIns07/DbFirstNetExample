global using DbFirst.Models;
using AutoMapper;
using DbFirst.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DbFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly LeagueDatabaseContext _leagueDatabaseContext;
        private readonly IMapper _iMapper;

        public TeamController(LeagueDatabaseContext leagueDatabaseContext, IMapper iMapper)
        {
            _leagueDatabaseContext = leagueDatabaseContext;
            _iMapper = iMapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Team>>> GetTeams()
        {
            return Ok(await _leagueDatabaseContext.Teams.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult> CreateTeam([FromBody] TeamDTO team)
        {
            if (team == null)
            {
                return BadRequest(ModelState);
            }

            var existingTeam = _leagueDatabaseContext.Teams.Where(c => c.Name.Trim().ToUpper() == team.Name.Trim().ToUpper()).ToListAsync();

            if (existingTeam != null)
            {
                ModelState.AddModelError("", "Song already exists");
                return StatusCode(422, ModelState);
            }

            var mappedTeam = _iMapper.Map<Team>(team);  

            return Ok(await _leagueDatabaseContext.Teams.AddAsync(mappedTeam));
        }
    }
}
