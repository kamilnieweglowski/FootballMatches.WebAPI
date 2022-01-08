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
    [Route("api/Players")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<PlayersController> _logger;

        public PlayersController(ApplicationDbContext context, IMapper mapper, ILogger<PlayersController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Players
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerDTO>>> GetPlayers()
        {
            try
            {
                var players = _mapper.Map<IEnumerable<PlayerDTO>>(await _context.Players.ToListAsync());
                return Ok(players);
            }
            catch (Exception ex)
            {
                _logger.LogError($"PlayersController -> GetPlayers Error: {ex.Message}");
                return BadRequest($"{BadRequest().StatusCode} : {ex.Message}");
            }
        }

        // GET: api/Players/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerDTO>> GetPlayer(int id)
        {
            try
            {
                var player = await _context.Players.FindAsync(id);

                if (player == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<PlayerDTO>(player));
            }
            catch (Exception ex)
            {
                _logger.LogError($"PlayersController -> GetPlayer Error: {ex.Message}");
                return BadRequest($"{BadRequest().StatusCode} : {ex.Message}");
            }
        }

        // PUT: api/Players/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayer(PlayerDTO player)
        {
            try
            {
                PrepareAndValidatePlayersData(ref player);

                _context.Entry(_mapper.Map<Player>(player)).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(player.Id))
                {
                    _logger.LogError($"PlayersController -> PutPlayer Error: player does not exist.");
                    return NotFound($"Player with id {player.Id} does not exist.");
                }
                else
                {
                    return Conflict();
                }
            }

            return Ok($"Player's data successfully modified!");
        }

        // POST: api/Players
        [HttpPost]
        public async Task<ActionResult<PlayerDTO>> PostPlayer(PlayerDTO player)
        {
            try
            {
                if (PlayerExists(player))
                    throw new Exception("Error: player with such personal data already exists.");

                PrepareAndValidatePlayersData(ref player);

                _context.Players.Add(_mapper.Map<Player>(player));
                await _context.SaveChangesAsync();

                return Ok($"Player {player.FirstName} {player.LastName} successfully added!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"PlayersController -> PostPlayer Error: {ex.Message}");
                return BadRequest($"{BadRequest().StatusCode} : {ex.Message}");
            }
        }

        // DELETE: api/Players/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            try
            {
                var player = await _context.Players.FindAsync(id);
                if (player == null)
                {
                    return NotFound($"There's no player with id {id}.");
                }

                _context.Players.Remove(player);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"PlayersController -> DeletePlayer Error: {ex.Message}");
                return BadRequest($"{BadRequest().StatusCode} : {ex.Message}");
            }
            return Ok($"Player with id {id} successfully deleted!");
        }

        private bool PlayerExists(int id)
        {
            return _context.Players.Any(e => e.Id == id);
        }
        private bool PlayerExists(PlayerDTO player)
        {
            return _context.Players.Any(p =>
                p.FirstName == player.FirstName
                && p.LastName == player.LastName
                && p.DateOfBirth.Date == player.DateOfBirth.Date
                && p.CountryCodeId == player.CountryCodeId);
        }

        private void PrepareAndValidatePlayersData(ref PlayerDTO player)
        {
            player.DateOfBirth = player.DateOfBirth.Date;

            var teamId = player.TeamId;

            var errors = string.Empty;

            Regex regex = new Regex("^[a-zA-ZżźćńółęąśŻŹĆĄŚĘŁÓŃ]+(?:[\\s-][a-zA-ZżźćńółęąśŻŹĆĄŚĘŁÓŃ]+)*$");

            if (!regex.IsMatch(player.FirstName))
                errors += "Error: Player's First Name appears to be wrong.\n";

            if (!regex.IsMatch(player.LastName))
                errors += "Error: Player's Last Name appears to be wrong.\n";

            var countryCodes = _context.CountryCodes.OrderBy(x => x.Id);
            var minId = countryCodes.First().Id;
            var maxId = countryCodes.Last().Id;

            if (player.CountryCodeId < minId || player.CountryCodeId > maxId)
                errors += "Error: CountryCodeId is not valid.";

            if (player.Position < Enum.GetValues(typeof(Positions)).Cast<int>().Min() 
                || player.Position > Enum.GetValues(typeof(Positions)).Cast<int>().Max())
                errors += "Error: Position has to be valid.\n";

            if (teamId != null && !_context.Teams.Any(t => t.Id == teamId))
                errors += $"Error: Team with id {player.TeamId} does not exist in the Database.";

            if (errors.Length > 0)
                throw new Exception(errors);
        }
    }
}
