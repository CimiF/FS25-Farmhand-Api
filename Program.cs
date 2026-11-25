using FS25Gateway.Application.Services;
using FS25Gateway.Application.Services.Mappers;
using FS25Gateway.Application.StateStore;
using FS25Gateway.Infrastructure.Transport;
using FS25Gateway.Infrastructure.Telemetry.Handlers;


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
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}

app.MapGet("/", () => "FS25 Gateway running");

app.MapGet("/health", () => new
{
	Status = "OK",
	Time = DateTime.Now
});

app.MapGet("/api/dashboard/snapshot", (StateStore stateStore) =>
	Results.Ok(stateStore.Read(state => state)));


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
