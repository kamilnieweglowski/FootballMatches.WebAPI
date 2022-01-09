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
using FootballMatches.WebAPI.SwaggerExamples;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Filters;

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

        /// <summary>
        /// Gets all Lineups
        /// </summary>
        [HttpGet]
        [Route("GetAllLineups")]
        public async Task<ActionResult<IEnumerable<LineupDTO>>> GetAllLineups()
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

        /// <summary>
        /// Gets specific Lineup by MatchId and TeamId
        /// </summary>
        [HttpGet]
        [Route("GetLineup")]
        public ActionResult<IEnumerable<LineupDTO>> GetLineup(int matchId, int teamId)
        {
            try
            {
                var lineup = _mapper.Map<IEnumerable<LineupDTO>>(_context.Lineups.Where(x => x.MatchId == matchId && x.TeamId == teamId).ToList());

                if (!lineup.Any())
                {
                    return NotFound();
                }

                return Ok(lineup);
            }
            catch (Exception ex)
            {
                _logger.LogError($"LineupsController -> GetLineup Error: {ex.Message}");
                return BadRequest($"{BadRequest().StatusCode} : {ex.Message}");
            }
        }

        /// <summary>
        /// Modifies specific Lineup
        /// </summary>
        [HttpPut]
        [Route("ModifyLineup")]
        public async Task<IActionResult> ModifyLineup(List<LineupDTO> lineupList)
        {
            try
            {
                ValidateLineup(lineupList);

                using (var dbContextTransaction = _context.Database.BeginTransaction())
                {
                    var existingLineups = _context.Lineups.Where(l => l.MatchId == lineupList.First().MatchId && l.TeamId == lineupList.First().TeamId).ToList();

                    if (existingLineups.Count > lineupList.Count)
                    {
                        var rowsToDelete = existingLineups.Where(p => !lineupList.Any(p2 => p2.Id == p.Id));

                        foreach (var row in rowsToDelete)
                        {
                            _context.Lineups.Remove(row);
                        }
                        _context.SaveChanges();
                    }
                    else if (existingLineups.Count < lineupList.Count)
                    {
                        var rowsToAdd = lineupList.Where(p => !existingLineups.Any(p2 => p2.Id == p.Id)).ToList();

                        foreach (var row in rowsToAdd)
                        {
                            row.Id = 0;
                            _context.Lineups.Add(_mapper.Map<Lineup>(row));
                        }
                        _context.SaveChanges();

                        lineupList = lineupList.Except(rowsToAdd).ToList();
                    }

                    var duplicates = lineupList.Select(x => x.Id).GroupBy(i => i).Where(g => g.Count() > 1);
                    if (duplicates.Any())
                        throw new Exception($"Error: Multiple rows has the same ids!.");

                    foreach (var lineup in lineupList)
                    {
                        if (!LineupExists(lineup.Id))
                            throw new Exception($"Lineup with id {lineup.Id} does not exist.");
                    }

                    foreach(var row in existingLineups)
                    {
                        var lineup = lineupList.FirstOrDefault(x => x.Id == row.Id);

                        if(lineup != null)
                        {
                            row.MatchId = lineup.MatchId;
                            row.TeamId = lineup.TeamId;
                            row.PlayerId = lineup.PlayerId;
                            row.IsOnBench = lineup.IsOnBench;
                        }
                    }

                    _context.SaveChanges();
                    dbContextTransaction.Commit();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict();
            }
            catch (Exception ex)
            {
                _logger.LogError($"LineupsController -> PutLineup Error: {ex.Message}");
                return BadRequest($"{BadRequest().StatusCode} : {ex.Message}");
            }

            return Ok($"Lineup's data successfully modified!");
        }

        /// <summary>
        /// Adds full one team Lineup for specific match
        /// </summary>
        [HttpPost]
        [Route("AddLineup")]
        public async Task<ActionResult> AddLineup(List<LineupDTO> lineupList)
        {
            try
            {
                if (LineupExists(lineupList.First()))
                    throw new Exception("Error: lineup with such match and team already exists.");

                ValidateLineup(lineupList);

                using (var dbContextTransaction = _context.Database.BeginTransaction())
                {
                    foreach (var lineup in lineupList)
                    {
                        lineup.Id = 0;
                        _context.Lineups.Add(_mapper.Map<Lineup>(lineup));
                    }

                    await _context.SaveChangesAsync();
                    dbContextTransaction.Commit();
                }

                return Ok($"Lineup successfully added!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"LineupsController -> PostLineup Error: {ex.Message}");
                return BadRequest($"{BadRequest().StatusCode} : {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes specific Lineup
        /// </summary>
        [HttpDelete]
        [Route("DeleteLineup")]
        public async Task<IActionResult> DeleteLineup(int matchId, int teamId)
        {
            try
            {
                var lineupList = _context.Lineups.Where(x => x.MatchId == matchId && x.TeamId == teamId).ToList();
                if (!lineupList.Any())
                {
                    return NotFound($"There's no lineup for matchId {matchId} and teamId {teamId}.");
                }

                using (var dbContextTransaction = _context.Database.BeginTransaction())
                {
                    foreach (var lineup in lineupList)
                        _context.Lineups.Remove(lineup);

                    _context.SaveChanges();
                    dbContextTransaction.Commit();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"LineupsController -> DeleteLineup Error: {ex.Message}");
                return BadRequest($"{BadRequest().StatusCode} : {ex.Message}");
            }
            return Ok($"Lineup successfully deleted!");
        }

        private bool LineupExists(int id)
        {
            return _context.Lineups.Any(e => e.Id == id);
        }

        private bool LineupExists(LineupDTO lineup)
        {
            return _context.Lineups.Any(l =>
                l.MatchId == lineup.MatchId
                && l.TeamId == lineup.TeamId);
        }

        private void ValidateLineup(List<LineupDTO> lineupList)
        {
            var errors = string.Empty;

            if (lineupList.GroupBy(x => x.MatchId).ToList().Count > 1 || lineupList.GroupBy(x => x.TeamId).ToList().Count > 1)
                throw new Exception($"Error: Not all rows has the same matchId/ teamId!");

            var matchId = lineupList.First().MatchId;
            var teamId = lineupList.First().TeamId;

            if (!_context.Matches.Any(m => m.Id == matchId))
                throw new Exception($"Error: Match with id {matchId} does not exist.\n");

            if (!_context.Teams.Any(m => m.Id == teamId))
                throw new Exception($"Error: Team with id {teamId} does not exist.\n");

            if (lineupList.Where(x => !x.IsOnBench).Count() != 11
                || lineupList.Count() > 18)
            {
                errors += $"Error: Wrong number of players (required 11 in main lineup + max. 7 on bench).\n";
            }

            foreach (var lineup in lineupList)
            {
                var player = _context.Players.FirstOrDefault(x => x.Id == lineup.PlayerId);

                if (player == null)
                {
                    errors += $"Error: Player with id {lineup.PlayerId} does not exist.\n";
                    continue;
                }

                if (player.TeamId != teamId)
                {
                    errors += $"Error: Player {player.Id} - {player.FirstName} {player.LastName} is from other team!.\n";
                    continue;
                }

                lineup.Position = player.Position;
            }

            var duplicates = lineupList.Select(x => x.PlayerId).GroupBy(i => i).Where(g => g.Count() > 1);
            if (duplicates.Any())
                errors += $"Error: Players with ids {duplicates.ToString} are duplicated!.\n";

            if (!lineupList.Any(x => !x.IsOnBench && x.Position == (int)Positions.Goalkeeper))
                errors += $"Error: There has to be at least one goalkeeper in the team!.\n";

            if (errors.Length > 0)
                throw new Exception(errors);
        }
    }
}
