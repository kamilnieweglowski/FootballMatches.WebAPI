#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using Entity = FootballMatches.Core.Entities;
using FootballMatches.Infrastructure.Data;
using FootballMatches.WebAPI.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FootballMatches.WebAPI.Controllers
{
    [Route("api/Matches")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<MatchesController> _logger;
        public MatchesController(ApplicationDbContext context, IMapper mapper, ILogger<MatchesController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Gets all Matches
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MatchDTO>>> GetMatches()
        {
            try
            {
                var matches = _mapper.Map<IEnumerable<MatchDTO>>(await _context.Matches.ToListAsync());
                return Ok(matches);
            }
            catch (Exception ex)
            {
                _logger.LogError($"MatchesController -> GetMatches Error: {ex.Message}");
                return BadRequest($"{BadRequest().StatusCode} : {ex.Message}");
            }
        }

        /// <summary>
        /// Gets a Match
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<MatchDTO>> GetMatch(int id)
        {
            try
            {
                var match = await _context.Matches.FindAsync(id);

                if (match == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<MatchDTO>(match));
            }
            catch (Exception ex)
            {
                _logger.LogError($"MatchesController -> GetMatch Error: {ex.Message}");
                return BadRequest($"{BadRequest().StatusCode} : {ex.Message}");
            }
        }

        /// <summary>
        /// Edits a Match
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMatch(MatchDTO match)
        {
            try
            {
                ValidateMatchsData(match);

                _context.Entry(_mapper.Map<Entity.Match>(match)).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchExists(match.Id))
                {
                    _logger.LogError($"MatchesController -> PutMatch Error: match does not exist.");
                    return NotFound($"Match with id {match.Id} does not exist.");
                }
                else
                {
                    return Conflict();
                }
            }

            return Ok($"Match's data successfully modified!");
        }

        /// <summary>
        /// Adds a Match
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> PostMatch(MatchDTO match)
        {
            try
            {
                if (MatchExists(match))
                    throw new Exception("Error: Match with such data already exists.");

                ValidateMatchsData(match);
                match.Id = 0;
                _context.Matches.Add(_mapper.Map<Entity.Match>(match));
                await _context.SaveChangesAsync();

                return Ok($"Match successfully added!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"MatchesController -> PostMatch Error: {ex.Message}");
                return BadRequest($"{BadRequest().StatusCode} : {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes a Match
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatch(int id)
        {
            try
            {
                var match = await _context.Matches.FindAsync(id);

                if (match == null)
                {
                    return NotFound($"There's no Match with id {id}.");
                }

                var lineups = _context.Lineups.Where(x => x.Match == match).ToList();

                _context.Matches.Remove(match);;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"MatchesController -> DeleteMatch Error: {ex.Message}");
                return BadRequest($"{BadRequest().StatusCode} : {ex.Message}");
            }
            return Ok($"Match with id {id} successfully deleted!");
        }

        private bool MatchExists(int id)
        {
            return _context.Matches.Any(e => e.Id == id);
        }
        private bool MatchExists(MatchDTO match)
        {
            return _context.Matches.Any(s =>
                s.Date.Date == match.Date.Date
                && s.HomeTeamId == match.HomeTeamId
                && s.AwayTeamId == match.AwayTeamId);
        }

        private void ValidateMatchsData(MatchDTO match)
        {
            var errors = string.Empty;

            if (!_context.Teams.Any(t => t.Id == match.HomeTeamId))
                errors += $"Error: HomeTeam with id {match.HomeTeamId} does not exist in the Database.\n";

            if (!_context.Teams.Any(t => t.Id == match.AwayTeamId))
                errors += $"Error: AwayTeam with id {match.AwayTeamId} does not exist in the Database.\n";

            if (match.AwayTeamId == match.HomeTeamId)
                errors += $"Error: HomeTeam cannot be the same as AwayTeam.\n";

            if (match.StadiumId != null && !_context.Stadiums.Any(t => t.Id == match.StadiumId))
                errors += $"Error: Stadium with id {match.StadiumId} does not exist in the Database.";

            if (errors.Length > 0)
                throw new Exception(errors);
        }
    }
}
