using Microsoft.EntityFrameworkCore;
using TeamApi.Models;

namespace TeamApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }

        public DbSet<CollegeProgram> CollegePrograms { get; set; }

        // Declare yours here also, eg:
        // public DbSet<Example> Example { get; set; }
    }
}
