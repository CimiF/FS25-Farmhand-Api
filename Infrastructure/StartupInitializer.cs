using System.Xml.Linq;

public sealed class StartupInitializer : IHostedService
{
	private readonly ILogger<StartupInitializer> _logger;

	public StartupInitializer(
		ILogger<StartupInitializer> logger)
	{
		_logger = logger;
	}

	public Task StartAsync(CancellationToken cancellationToken)
	{
		_logger.LogInformation("Initializing application");

		WriteSessionAsync(cancellationToken).GetAwaiter().GetResult();
		// načíst konfiguraci
		// zapsat startup informace

		return Task.CompletedTask;
	}

	public Task StopAsync(CancellationToken cancellationToken)
	{
		return Task.CompletedTask;
	}

	public async Task WriteSessionAsync(
		CancellationToken cancellationToken = default)
	{
		var sessionId = DateTimeOffset.UtcNow
			.ToUnixTimeMilliseconds()
			.ToString();
		var settingsFolder = @"c:\Users\filip\AppData\Local\Packages\GIANTSSoftware.FarmingSimulator25PC_fa8jxm5fj0esw\LocalCache\Local\modSettings\FS25_Farmhand\";
		/*Path.Combine(
			Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
			"My Games",
			"FarmingSimulator2025",
			"modSettings",
			"FS25_Farmhand");*/

		Directory.CreateDirectory(settingsFolder);

		var sessionPath = Path.Combine(
			settingsFolder,
			"session_edge.xml");

		var document = new XDocument(
			new XDeclaration("1.0", "utf-8", "no"),
			new XElement(
				"session",
				new XAttribute("id", sessionId)));

		await using var stream = File.Create(sessionPath);

		document.Save(stream);

		_logger.LogInformation(
			"Farmhand session written. Id={SessionId}, Path={Path}",
			sessionId,
			sessionPath);
	}
}
