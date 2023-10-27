using Microsoft.EntityFrameworkCore;
using test_background_api.dbContext.AppDbContext;
using test_background_api.dbContext.Quartz;

namespace test_background_api.Middleware;

public class EnsureDatabaseMiddleware
{
    private readonly RequestDelegate _next;

    public EnsureDatabaseMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, AppDbContext dbContext,QuartzDbContext quartzDbContext)
    {
        dbContext.Database.Migrate();  
        quartzDbContext.Database.Migrate();
        await _next(context);
    }
}