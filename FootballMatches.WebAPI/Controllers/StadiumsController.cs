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
    [Route("api/Stadiums")]
    [ApiController]
    public class StadiumsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<StadiumsController> _logger;
        public StadiumsController(ApplicationDbContext context, IMapper mapper, ILogger<StadiumsController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Gets all Stadiums
        /// </summary>
        [HttpGet]
        [Route("GetAllStadiums")]
        public async Task<ActionResult<IEnumerable<StadiumDTO>>> GetAllStadiums()
        {
            try
            {
                var stadiums = _mapper.Map<IEnumerable<StadiumDTO>>(await _context.Stadiums.ToListAsync());
                return Ok(stadiums);
            }
            catch (Exception ex)
            {
                _logger.LogError($"StadiumsController -> GetStadiums Error: {ex.Message}");
                return BadRequest($"{BadRequest().StatusCode} : {ex.Message}");
            }
        }

        /// <summary>
        /// Gets a Stadium by its ID
        /// </summary>
        [HttpGet]
        [Route("GetStadium")]
        public async Task<ActionResult<StadiumDTO>> GetStadium(int id)
        {
            try
            {
                var stadium = await _context.Stadiums.FindAsync(id);

                if (stadium == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<StadiumDTO>(stadium));
            }
            catch (Exception ex)
            {
                _logger.LogError($"StadiumsController -> GetStadium Error: {ex.Message}");
                return BadRequest($"{BadRequest().StatusCode} : {ex.Message}");
            }
        }

        /// <summary>
        /// Edits a Stadium
        /// </summary>
        [HttpPut]
        [Route("EditStadium")]
        public async Task<IActionResult> EditStadium(StadiumDTO stadium)
        {
            try
            {
                ValidateStadiumsData(ref stadium);

                _context.Entry(_mapper.Map<Stadium>(stadium)).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StadiumExists(stadium.Id))
                {
                    _logger.LogError($"StadiumsController -> PutStadium Error: countryCode does not exist.");
                    return NotFound($"Stadium with id {stadium.Id} does not exist.");
                }
                else
                {
                    return Conflict();
                }
            }

            return Ok($"Stadium's data successfully modified!");
        }

        /// <summary>
        /// Adds a Stadium
        /// </summary>
        [HttpPost]
        [Route("AddStadium")]
        public async Task<ActionResult> AddStadium(StadiumDTO stadium)
        {
            try
            {
                if (StadiumExists(stadium))
                    throw new Exception("Error: Stadium with such data already exists.");

                ValidateStadiumsData(ref stadium);
                stadium.Id = 0;
                _context.Stadiums.Add(_mapper.Map<Stadium>(stadium));
                await _context.SaveChangesAsync();

                return Ok($"Stadium {stadium.Name} successfully added!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"StadiumsController -> PostStadium Error: {ex.Message}");
                return BadRequest($"{BadRequest().StatusCode} : {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes a Stadium by its ID
        /// </summary>
        [HttpDelete]
        [Route("DeleteStadium")]
        public async Task<IActionResult> DeleteStadium(int id)
        {
            try
            {
                var stadium = _context.Stadiums.Where(x => x.Id == id).Include(e => e.Teams).Include(t => t.Matches).FirstOrDefault();

                if (stadium == null)
                {
                    return NotFound($"There's no Stadium with id {id}.");
                }

                stadium.Teams.Clear();
                stadium.Matches.Clear();

                _context.Stadiums.Remove(stadium);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"StadiumsController -> DeleteStadium Error: {ex.Message}");
                return BadRequest($"{BadRequest().StatusCode} : {ex.Message}");
            }
            return Ok($"Stadium with id {id} successfully deleted!");
        }

        private bool StadiumExists(int id)
        {
            return _context.Stadiums.Any(e => e.Id == id);
        }
        private bool StadiumExists(StadiumDTO stadium)
        {
            return _context.Stadiums.Any(s =>
                s.Name == stadium.Name
                && s.City == stadium.City
                && s.CountryCodeId == stadium.CountryCodeId);
        }

        private void ValidateStadiumsData(ref StadiumDTO stadium)
        {
            var errors = string.Empty;

            Regex regex = new Regex("^[a-zA-ZżźćńółęąśŻŹĆĄŚĘŁÓŃ]+(?:[\\s-][a-zA-ZżźćńółęąśŻŹĆĄŚĘŁÓŃ]+)*$");

            if (!regex.IsMatch(stadium.City))
                errors += "Error: Stadium's City appears to be wrong.\n";

            if (String.IsNullOrEmpty(stadium.Name.Trim()))
                errors += "Error: Stadium's Name cannot be empty.\n";

            var countryCodes = _context.CountryCodes.OrderBy(x => x.Id);
            var minId = countryCodes.First().Id;
            var maxId = countryCodes.Last().Id;

            if (stadium.CountryCodeId < minId || stadium.CountryCodeId > maxId)
                errors += "Error: CountryCodeId is not valid.";

            if (errors.Length > 0)
                throw new Exception(errors);
        }
    }
}
