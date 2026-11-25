using System.Net.Http.Json;
using FS25Gateway.Web.Models;

namespace FS25Gateway.Web.Services;

/// <summary>Provides typed access to the FS25Gateway API.</summary>
public sealed class FarmApiClient(HttpClient httpClient)
{
	private const string DashboardSnapshotPath = "api/dashboard/snapshot";

	/// <summary>Loads the current dashboard snapshot.</summary>
	/// <param name="cancellationToken">The request cancellation token.</param>
	/// <returns>The dashboard snapshot, or null when the API returns no content.</returns>
	public async Task<DashboardSnapshotDto?> GetDashboardSnapshotAsync(CancellationToken cancellationToken = default)
	{
		using var response = await httpClient.GetAsync(DashboardSnapshotPath, cancellationToken);
		response.EnsureSuccessStatusCode();
		return await response.Content.ReadFromJsonAsync<DashboardSnapshotDto>(cancellationToken: cancellationToken);
	}
}
