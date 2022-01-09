#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using FootballMatches.Core.Entities;
using FootballMatches.Core.Enums;
using FootballMatches.Infrastructure.Data;
using FootballMatches.WebAPI.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FootballMatches.WebAPI.Controllers
{
    [Route("api/Lineups")]
    [ApiController]
    public class LineupsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<LineupsController> _logger;

        public LineupsController(ApplicationDbContext context, IMapper mapper, ILogger<LineupsController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Lineups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LineupDTO>>> GetLineups()
        {
            try
            {
                var lineups = _mapper.Map<IEnumerable<LineupDTO>>(await _context.Lineups.ToListAsync());
                return Ok(lineups);
            }
            catch (Exception ex)
            {
                _logger.LogError($"LineupsController -> GetLineups Error: {ex.Message}");
                return BadRequest($"{BadRequest().StatusCode} : {ex.Message}");
            }
        }

        // GET: api/Lineups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LineupDTO>> GetLineup(int id)
        {
            try
            {
                var lineup = await _context.Lineups.FindAsync(id);

                if (lineup == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<LineupDTO>(lineup));
            }
            catch (Exception ex)
            {
                _logger.LogError($"LineupsController -> GetLineup Error: {ex.Message}");
                return BadRequest($"{BadRequest().StatusCode} : {ex.Message}");
            }
        }

        // PUT: api/Lineups/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLineup(LineupDTO lineup)
        {
            try
            {
                ValidateLineupsData(lineup);

                _context.Entry(_mapper.Map<Lineup>(lineup)).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LineupExists(lineup.Id))
                {
                    _logger.LogError($"LineupsController -> PutLineup Error: lineup does not exist.");
                    return NotFound($"Lineup with id {lineup.Id} does not exist.");
                }
                else
                {
                    return Conflict();
                }
            }

            return Ok($"Lineup's data successfully modified!");
        }

        // POST: api/Lineups
        [HttpPost]
        public async Task<ActionResult> PostLineup(LineupDTO lineup)
        {
            try
            {
                if (LineupExists(lineup))
                    throw new Exception("Error: lineup with such data already exists.");

                ValidateLineupsData(lineup);

                _context.Lineups.Add(_mapper.Map<Lineup>(lineup));
                await _context.SaveChangesAsync();

                return Ok($"Lineup successfully added!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"LineupsController -> PostLineup Error: {ex.Message}");
                return BadRequest($"{BadRequest().StatusCode} : {ex.Message}");
            }
        }

        // DELETE: api/Lineups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLineup(int id)
        {
            try
            {
                var lineup = await _context.Lineups.FindAsync(id);
                if (lineup == null)
                {
                    return NotFound($"There's no lineup with id {id}.");
                }

                _context.Lineups.Remove(lineup);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"LineupsController -> DeleteLineup Error: {ex.Message}");
                return BadRequest($"{BadRequest().StatusCode} : {ex.Message}");
            }
            return Ok($"Lineup with id {id} successfully deleted!");
        }

        private bool LineupExists(int id)
        {
            return _context.Lineups.Any(e => e.Id == id);
        }
        private bool LineupExists(LineupDTO lineup)
        {
            return _context.Lineups.Any(l =>
                l.MatchId == lineup.MatchId
                && l.TeamId == lineup.TeamId
                && l.PlayerId == lineup.PlayerId);
        }

        private void ValidateLineupsData(LineupDTO lineup)
        {
            var errors = string.Empty;

            if (!_context.Matches.Any(m => m.Id == lineup.MatchId))
                errors += $"Error: Match with id {lineup.MatchId} does not exist.\n";

            if (!_context.Teams.Any(m => m.Id == lineup.TeamId))
                errors += $"Error: Team with id {lineup.TeamId} does not exist.\n";

            if (!_context.Players.Any(m => m.Id == lineup.PlayerId))
                errors += $"Error: Player with id {lineup.PlayerId} does not exist.";

            if (errors.Length > 0)
                throw new Exception(errors);
        }
    }
}
