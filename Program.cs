using FS25Gateway.Application.Services;
using FS25Gateway.Application.Services.Mappers;
using FS25Gateway.Application.StateStore;
using FS25Gateway.Infrastructure.Transport;
using FS25Gateway.Infrastructure.Telemetry.Handlers;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<StateStore>();
builder.Services.AddSingleton<WorkersFrameHandler>();
builder.Services.AddSingleton<FieldsFrameMapper>();
builder.Services.AddSingleton<SitesFrameMapper>();
builder.Services.AddSingleton<StorageFrameMapper>();
builder.Services.AddSingleton<MissionsFrameMapper>();
builder.Services.AddSingleton<WeatherFrameMapper>();
builder.Services.AddSingleton<SettingsFrameMapper>();
builder.Services.AddSingleton<PlayersFrameMapper>();
builder.Services.AddSingleton<FarmStateMapper>();
builder.Services.AddSingleton<NoOpFarmhandFrameHandlers>();
builder.Services.AddSingleton<IWorkersFrameHandler>(serviceProvider => serviceProvider.GetRequiredService<WorkersFrameHandler>());
builder.Services.AddSingleton<IMissionsFrameHandler>(serviceProvider => serviceProvider.GetRequiredService<NoOpFarmhandFrameHandlers>());
builder.Services.AddSingleton<IFieldsFrameHandler>(serviceProvider => serviceProvider.GetRequiredService<NoOpFarmhandFrameHandlers>());
builder.Services.AddSingleton<IStorageFrameHandler>(serviceProvider => serviceProvider.GetRequiredService<NoOpFarmhandFrameHandlers>());
builder.Services.AddSingleton<ISitesFrameHandler>(serviceProvider => serviceProvider.GetRequiredService<NoOpFarmhandFrameHandlers>());
builder.Services.AddSingleton<IWeatherFrameHandler>(serviceProvider => serviceProvider.GetRequiredService<NoOpFarmhandFrameHandlers>());
builder.Services.AddSingleton<ISettingsFrameHandler>(serviceProvider => serviceProvider.GetRequiredService<NoOpFarmhandFrameHandlers>());
builder.Services.AddSingleton<IPlayersFrameHandler>(serviceProvider => serviceProvider.GetRequiredService<NoOpFarmhandFrameHandlers>());
builder.Services.AddSingleton<FarmhandFrameRouter>();
builder.Services.AddSingleton<IFarmhandFrameReceiver>(serviceProvider => serviceProvider.GetRequiredService<FarmhandFrameRouter>());
builder.Services.AddHostedService<FarmhandPipeReader>();
builder.Services.AddHostedService<StartupInitializer>();

builder.Host.UseSerilog();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.Configure<HostOptions>(options =>
{
	options.BackgroundServiceExceptionBehavior =
		BackgroundServiceExceptionBehavior.Ignore;
});

Log.Logger = new LoggerConfiguration()
	.MinimumLevel.Information()
	.WriteTo.Console()
	.WriteTo.File(
	"logs/fs25gateway-.log",
	rollingInterval: RollingInterval.Day,
	outputTemplate:
		"{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
		retainedFileCountLimit: 14)
	.CreateLogger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}

app.MapGet("/api/dashboard/snapshot", (StateStore stateStore) =>
	Results.Ok(stateStore.Read(state => state)));


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
