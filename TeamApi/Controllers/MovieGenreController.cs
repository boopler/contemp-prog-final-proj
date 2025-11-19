using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamApi.Data;
using TeamApi.Models;

namespace TeamApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieGenresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MovieGenresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/MovieGenres?id=0 or no id -> first 5
        // GET: api/MovieGenres?id=3        -> only id 3
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieGenre>>> GetMovieGenres([FromQuery] int? id)
        {
            if (id == null || id == 0)
            {
                return await _context.MovieGenres
                    .OrderBy(m => m.Id)
                    .Take(5)
                    .ToListAsync();
            }

            var genre = await _context.MovieGenres.FindAsync(id.Value);
            if (genre == null)
            {
                return NotFound();
            }

            return new List<MovieGenre> { genre };
        }

        // POST: api/MovieGenres
        [HttpPost]
        public async Task<ActionResult<MovieGenre>> PostMovieGenre(MovieGenre movieGenre)
        {
            _context.MovieGenres.Add(movieGenre);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMovieGenres), new { id = movieGenre.Id }, movieGenre);
        }

        // PUT: api/MovieGenres/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovieGenre(int id, MovieGenre movieGenre)
        {
            if (id != movieGenre.Id)
            {
                return BadRequest();
            }

            _context.Entry(movieGenre).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.MovieGenres.Any(e => e.Id == id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // DELETE: api/MovieGenres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovieGenre(int id)
        {
            var genre = await _context.MovieGenres.FindAsync(id);
            if (genre == null)
            {
                return NotFound();
            }

            _context.MovieGenres.Remove(genre);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}