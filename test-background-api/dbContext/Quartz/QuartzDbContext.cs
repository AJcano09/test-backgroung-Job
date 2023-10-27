using Microsoft.EntityFrameworkCore;

namespace test_background_api.dbContext.Quartz;

public class QuartzDbContext : DbContext
{
    public QuartzDbContext(DbContextOptions<QuartzDbContext> options): base(options)
    {
        
    }
}