using Microsoft.EntityFrameworkCore;
using Quartz;
using Serilog;
using test_background_api.AutoMapper;
using test_background_api.dbContext.AppDbContext;
using test_background_api.dbContext.Quartz;
using test_background_api.Jobs;
using test_background_api.Repository;
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
Log.Information("App started...");
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
var configuration = builder.Configuration;
builder.Services.AddSerilog();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddTransient<SaveMessageJob>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("AppDbContextConnectionString"),
        o =>
        {
            o.SetPostgresVersion(10, 6);
            o.MigrationsAssembly("test-background-api");
            o.MigrationsHistoryTable("__EFMigrationsHistory", "public");
        }));

builder.Services.AddDbContext<QuartzDbContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("QuartzConnectionString"),
        o =>
        {
            o.SetPostgresVersion(10, 6);
            o.MigrationsAssembly("test-background-api");
            o.MigrationsHistoryTable("__EFMigrationsHistory", "quartz");
        }));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddQuartz(q =>
{
    var jobKey = new JobKey(nameof(SaveMessageJob));
    q.AddJob<SaveMessageJob>(x =>
    {
        x.WithIdentity(jobKey);
        x.StoreDurably();
        x.RequestRecovery();
    });
    q.UsePersistentStore(x =>
    {
        x.UsePostgres(configuration.GetConnectionString("QuartzConnectionString") ?? string.Empty);
        x.UseNewtonsoftJsonSerializer();
    });
});

builder.Services.AddQuartzHostedService(options =>
{
    options.AwaitApplicationStarted = true;
    options.WaitForJobsToComplete = true;
});
builder.Services.AddSingleton<JobHandler>();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

//app.UseMiddleware<EnsureDatabaseMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();