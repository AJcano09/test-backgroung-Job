using Microsoft.EntityFrameworkCore;
using test_background_api.dbContext.Entities;

namespace test_background_api.dbContext.AppDbContext;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
    {
    }
    public DbSet<Message?> Messages { get; set; }
}