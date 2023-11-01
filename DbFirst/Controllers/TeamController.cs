global using DbFirst.Models;
using AutoMapper;
using DbFirst.Interfaces;
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

        public TeamController(LeagueDatabaseContext teamRepository, IMapper mapper)
        {
            _leagueDatabaseContext = teamRepository;
            _iMapper = mapper;
        }

        [HttpGet]
        [ResponseCache(Duration = 3600)]
        public async Task<ActionResult<List<Team>>> GetTeams([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var teams = await _leagueDatabaseContext.Teams.Skip((page - 1) * pageSize)
                 .Take(pageSize).ToListAsync();
                return Ok(teams);

            }
            catch (Exception)
            {
                return StatusCode(500, "Error to obtain the list of teams.");

            }
        }

        [HttpGet("IdTeam")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<TeamDTO>> GetTeamByID([FromQuery] int IdTeam)
        {
            var existingTeam = await _leagueDatabaseContext.Teams.SingleOrDefaultAsync(c => c.Id == IdTeam);
            if (existingTeam == null)
            {
                return BadRequest(ModelState);
            }

            return Ok(existingTeam);
        }

        [HttpPost("Create")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(422)]
        public async Task<ActionResult<TeamDTO>> CreateTeam([FromBody] TeamDTO team)
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
                ModelState.AddModelError("", "Team already exists");
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
        public async Task<ActionResult<TeamDTO>> DeleteTeam([FromQuery] int Id)
        {
            //FindAsync busca el atributo en la base de datos. Nombrar exactamente como se localiza en la base de datos
            var existingTeam = await _leagueDatabaseContext.Teams.FindAsync(Id);

            if (existingTeam == null)
            {
                return NotFound();
            }

            _leagueDatabaseContext.Teams.Remove(existingTeam);
            await _leagueDatabaseContext.SaveChangesAsync();

            return Ok(new { message = "The team has been removed with success!" });

        }

        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<TeamDTO>> UpdateTeam([FromQuery] int Id, [FromBody] TeamDTO team)
        {
            if (team == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var existingTeam = await _leagueDatabaseContext.Teams.FindAsync(Id);

                if (existingTeam == null)
                {
                    return NotFound();
                }
                _iMapper.Map(team, existingTeam);

                _leagueDatabaseContext.Update(existingTeam);
                

                return NoContent();

            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "Error al actualizar el equipo." + ex.Message);

            }

        }
        public async Task<ActionResult<TeamDTO>> TeamExists(int Id)
        {
            var existingTeam = await _leagueDatabaseContext.Teams.FindAsync(Id);

            if (existingTeam == null)
            {
                return NotFound();
            }

            return Ok(existingTeam);
        }
    }
}
