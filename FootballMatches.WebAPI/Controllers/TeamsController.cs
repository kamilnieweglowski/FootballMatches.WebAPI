#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using FootballMatches.Core.Entities;
using FootballMatches.Infrastructure.Data;
using FootballMatches.WebAPI.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FootballMatches.WebAPI.Controllers
{
    [Route("api/Teams")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<TeamsController> _logger;
        public TeamsController(ApplicationDbContext context, IMapper mapper, ILogger<TeamsController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Gets all Teams
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamDTO>>> GetTeams()
        {
            try
            {
                var teams = _mapper.Map<IEnumerable<TeamDTO>>(await _context.Teams.ToListAsync());
                return Ok(teams);
            }
            catch (Exception ex)
            {
                _logger.LogError($"TeamsController -> GetTeams Error: {ex.Message}");
                return BadRequest($"{BadRequest().StatusCode} : {ex.Message}");
            }
        }

        /// <summary>
        /// Gets a Team
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<TeamDTO>> GetTeam(int id)
        {
            try
            {
                var team = await _context.Teams.FindAsync(id);

                if (team == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<TeamDTO>(team));
            }
            catch (Exception ex)
            {
                _logger.LogError($"TeamsController -> GetTeam Error: {ex.Message}");
                return BadRequest($"{BadRequest().StatusCode} : {ex.Message}");
            }
        }

        /// <summary>
        /// Edits a Team
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeam(TeamDTO team)
        {
            try
            {
                ValidateTeamsData(ref team);

                _context.Entry(_mapper.Map<Team>(team)).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(team.Id))
                {
                    _logger.LogError($"TeamsController -> PutTeam Error: team does not exist.");
                    return NotFound($"Team with id {team.Id} does not exist.");
                }
                else
                {
                    return Conflict();
                }
            }

            return Ok($"Team's data successfully modified!");
        }

        /// <summary>
        /// Adds a Team
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> PostTeam(TeamDTO team)
        {
            try
            {
                if (TeamExists(team))
                    throw new Exception("Error: Team with such data already exists.");

                ValidateTeamsData(ref team);
                team.Id = 0;
                _context.Teams.Add(_mapper.Map<Team>(team));
                await _context.SaveChangesAsync();

                return Ok($"Team {team.Name} successfully added!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"TeamsController -> PostTeam Error: {ex.Message}");
                return BadRequest($"{BadRequest().StatusCode} : {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes a Team
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            try
            {
                var team = await _context.Teams.FindAsync(id);

                if (team == null)
                {
                    return NotFound($"There's no Team with id {id}.");
                }

                team = _context.Teams.Where(x => x.Id == id).Include(e => e.Players).FirstOrDefault();
                team.Players.Clear();

                var homeMatches = _context.Matches.Where(x => x.HomeTeam == team).Include(y => y.Lineups).ToList();
                var awayMatches = _context.Matches.Where(x => x.AwayTeam == team).Include(y => y.Lineups).ToList();

                _context.Teams.Remove(team);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"TeamsController -> DeleteTeam Error: {ex.Message}");
                return BadRequest($"{BadRequest().StatusCode} : {ex.Message}");
            }
            return Ok($"Team with id {id} successfully deleted!");
        }

        private bool TeamExists(int id)
        {
            return _context.Teams.Any(e => e.Id == id);
        }
        private bool TeamExists(TeamDTO team)
        {
            return _context.Teams.Any(s =>
                s.Name == team.Name
                && s.City == team.City
                && s.CountryCodeId == team.CountryCodeId);
        }

        private void ValidateTeamsData(ref TeamDTO team)
        {
            var errors = string.Empty;

            Regex regex = new Regex("^[a-zA-ZżźćńółęąśŻŹĆĄŚĘŁÓŃ]+(?:[\\s-][a-zA-ZżźćńółęąśŻŹĆĄŚĘŁÓŃ]+)*$");

            if (String.IsNullOrEmpty(team.Name.Trim()))
                errors += "Error: Team's Name cannot be empty.\n";

            if (!regex.IsMatch(team.City))
                errors += "Error: Team's City appears to be wrong.\n";

            var countryCodes = _context.CountryCodes.OrderBy(x => x.Id);
            var minId = countryCodes.First().Id;
            var maxId = countryCodes.Last().Id;

            if (team.CountryCodeId < minId || team.CountryCodeId > maxId)
                errors += "Error: CountryCodeId is not valid.";

            var stadiumId = team.StadiumId;
            if (stadiumId != null && !_context.Stadiums.Any(t => t.Id == stadiumId))
                errors += $"Error: Stadium with id {stadiumId} does not exist in the Database.";

            if (errors.Length > 0)
                throw new Exception(errors);
        }
    }
}
