using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamApi.Data;
using TeamApi.Models;

namespace TeamApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CollegeProgramController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public CollegeProgramController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CollegeProgram>>> FindCollegePrograms(int? programNameID)
        {
            if (programNameID == null || programNameID == 0)
            {
                var fiveResults = await context.CollegePrograms
                    .OrderBy(c => c.CollegeProgramPK)
                    .Take(5)
                    .ToListAsync();
                return Ok(fiveResults);
            }
            var cProgram = await context.CollegePrograms
            .FirstOrDefaultAsync(c => c.CollegeProgramPK == programNameID);
            if (cProgram == null)
            {
                return NotFound();
            }
            return Ok(new List<CollegeProgram> { cProgram });
        }
        [HttpPut("{programNameID}")]
        public async Task<ActionResult<CollegeProgram>> PutCollegeProgram(int programNameID, CollegeProgram collegeProgram)
        {
            if (programNameID != collegeProgram.CollegeProgramPK)
            {
                return BadRequest();
            }
            context.CollegePrograms.Update(collegeProgram);
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPost]
        public async Task<ActionResult<CollegeProgram>> PostCollegeProgram(CollegeProgram collegeProgram)
        {
            context.CollegePrograms.Add(collegeProgram);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(FindCollegePrograms),
                new
                {
                    programNameID = collegeProgram.CollegeProgramPK
                }, collegeProgram
                );
        }
        [HttpDelete("{programNameID}")]
        public async Task<IActionResult> DeleteCollegeProgram(string programNameID)
        {
            var collegeProgram = await context.CollegePrograms.FindAsync(programNameID);
            if (collegeProgram == null)
            {
                return NotFound();
            }
            context.CollegePrograms.Remove(collegeProgram);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
