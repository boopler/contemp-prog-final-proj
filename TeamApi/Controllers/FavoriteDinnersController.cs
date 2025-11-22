using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FavoriteDinnerApi.Models;
using TeamApi.Data;

namespace FavoriteDinnerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoriteDinnersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FavoriteDinnersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FavoriteDinner>>> GetFavoriteDinners([FromQuery] int? id)
        {
            if (id == null || id == 0)
            {
                return await _context.favoriteDinners
                    .OrderBy(fd => fd.Id)
                    .Take(5)
                    .ToListAsync();
            }

            var dinner = await _context.favoriteDinners.FindAsync(id.Value);
            if (dinner == null)
            {
                return NotFound();
            }
            return Ok(new List<FavoriteDinner> { dinner });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FavoriteDinner>> GetFavoriteDinner(int id)
        {
            var dinner = await _context.favoriteDinners.FindAsync(id);
            if (dinner == null)
            {
                return NotFound();
            }
            return dinner;
        }


        [HttpPost]
        public async Task<ActionResult<FavoriteDinner>> PostFavoriteDinner(FavoriteDinner dinner)
        {
            _context.favoriteDinners.Add(dinner);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetFavoriteDinner), new { id = dinner.Id }, dinner);
        }

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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavoriteDinner(int id)
        {
            var dinner = await _context.favoriteDinners.FindAsync(id);
            if (dinner == null)
            {
                return NotFound();
            }
            _context.favoriteDinners.Remove(dinner);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        private bool FavoriteDinnerExists(int id)
        {
            return _context.favoriteDinners.Any(e => e.Id == id);
        }
    }
}