using Microsoft.EntityFrameworkCore;
using Quiz.Models;

namespace Quiz.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<QuizEntry> QuizEntries { get; set; }
    }
}
