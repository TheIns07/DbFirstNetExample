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

        [HttpGet("ID")]
        public ActionResult<Team> GetTeamByID([FromQuery] int IdTeam)
        {
            var existingTeam = _leagueDatabaseContext.Teams.Where(c => c.Id == IdTeam);
            if (!existingTeam.Any())
            {
                return BadRequest(ModelState);
            }

            return Ok(existingTeam);
        }

        [HttpPost("Create")]
        public async Task<ActionResult> CreateTeam([FromBody] TeamDTO team)
        {
            if (team == null)
            {
                return BadRequest(ModelState);
            }

            var existingTeam = await _leagueDatabaseContext.Teams
                .Where(c => c.Name.Trim().ToUpper() == team.Name.Trim().ToUpper())
                .FirstOrDefaultAsync();

            if (existingTeam != null)
            {
                ModelState.AddModelError("", "El equipo ya existe");
                return BadRequest(ModelState); 
            }

            var mappedTeam = _iMapper.Map<Team>(team);

            await _leagueDatabaseContext.Teams.AddAsync(mappedTeam);
            await _leagueDatabaseContext.SaveChangesAsync();

            return Ok(mappedTeam);

        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> DeleteTeam([FromQuery] int Id)
        {
            //FindAsync busca el atributo en la base de datos. Nombrar exactamente como se localiza en la base de datos
            var existingTeam = await _leagueDatabaseContext.Teams.FindAsync(Id);

            if (existingTeam == null)
            {
                return NotFound();
            }
                
            _leagueDatabaseContext.Teams.Remove(existingTeam);
            await _leagueDatabaseContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> UpdateTeam([FromQuery] int Id, [FromBody] TeamDTO team)
        {
            if (team == null)
            {
                return BadRequest(ModelState);
            }

            var existingTeam = await _leagueDatabaseContext.Teams.FindAsync(Id);

            if (existingTeam == null)
            {
                return NotFound(); 
            }

            existingTeam.Name = team.Name;

            _leagueDatabaseContext.Update(existingTeam);
            await _leagueDatabaseContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
