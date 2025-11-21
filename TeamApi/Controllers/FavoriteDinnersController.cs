using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FavoriteDinnerApi.Data;
using FavoriteDinnerApi.Models;

namespace FavoriteDinnerApi.Controllers
{
    /// <summary>
    /// API controller providing CRUD operations for the FavoriteDinner table.  
    /// The GET endpoints accept an optional id parameter; if null or zero, the first
    /// five records are returned as required by the project specification.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class FavoriteDinnersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Constructs the controller with a database context resolved via dependency injection.
        /// </summary>
        /// <param name="context">The application's database context.</param>
        public FavoriteDinnersController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves favourite dinners.  
        /// If id is null or 0, returns the first five records; otherwise returns the record
        /// matching the provided id or a 404 if none found.
        /// </summary>
        /// <param name="id">Optional id of the favourite dinner to retrieve.</param>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FavoriteDinner>>> GetFavoriteDinners([FromQuery] int? id)
        {
            if (id == null || id == 0)
            {
                return await _context.FavoriteDinners
                    .OrderBy(fd => fd.Id)
                    .Take(5)
                    .ToListAsync();
            }

            var dinner = await _context.FavoriteDinners.FindAsync(id.Value);
            if (dinner == null)
            {
                return NotFound();
            }
            return Ok(new List<FavoriteDinner> { dinner });
        }

        /// <summary>
        /// Retrieves a favourite dinner by id using a route parameter.
        /// </summary>
        /// <param name="id">The id of the favourite dinner.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<FavoriteDinner>> GetFavoriteDinner(int id)
        {
            var dinner = await _context.FavoriteDinners.FindAsync(id);
            if (dinner == null)
            {
                return NotFound();
            }
            return dinner;
        }

        /// <summary>
        /// Creates a new favourite dinner record.
        /// </summary>
        /// <param name="dinner">The favourite dinner entity to create.</param>
        [HttpPost]
        public async Task<ActionResult<FavoriteDinner>> PostFavoriteDinner(FavoriteDinner dinner)
        {
            _context.FavoriteDinners.Add(dinner);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetFavoriteDinner), new { id = dinner.Id }, dinner);
        }

        /// <summary>
        /// Updates an existing favourite dinner record.
        /// </summary>
        /// <param name="id">Id of the record to update.</param>
        /// <param name="dinner">The updated favourite dinner entity.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFavoriteDinner(int id, FavoriteDinner dinner)
        {
            if (id != dinner.Id)
            {
                return BadRequest();
            }
            _context.Entry(dinner).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FavoriteDinnerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        /// <summary>
        /// Deletes a favourite dinner record.
        /// </summary>
        /// <param name="id">Id of the record to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavoriteDinner(int id)
        {
            var dinner = await _context.FavoriteDinners.FindAsync(id);
            if (dinner == null)
            {
                return NotFound();
            }
            _context.FavoriteDinners.Remove(dinner);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Determines whether a favourite dinner exists by id.
        /// </summary>
        private bool FavoriteDinnerExists(int id)
        {
            return _context.FavoriteDinners.Any(e => e.Id == id);
        }
    }
}