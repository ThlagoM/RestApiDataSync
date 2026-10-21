using RestApiDataSync.Services;
using RestApiDataSync.Data;

Console.WriteLine("=== REST API Data Sync ===");
using AppDbContext db = new();

await db.Database.EnsureCreatedAsync();

Console.WriteLine("SQLite database ready.");

using HttpClient httpClient = new()
{
    Timeout = TimeSpan.FromSeconds(15)
};

ProductApiService apiService = new(httpClient);

try
{
    var products = await apiService.GetProductsAsync();

    Console.WriteLine($"Products received: {products.Count}");

    if (products.Count == 0)
    {
        Console.WriteLine("No products received. Synchronization skipped.");
        return;
    }

    ProductSyncService syncService = new(db);

    await syncService.SyncAsync(products);

    foreach (var product in products.Take(5))
    {
        Console.WriteLine(
            $"ID: {product.Id} | {product.Title} | ${product.Price:F2}");
    }
}
catch (HttpRequestException ex)
{
    Console.WriteLine($"API request failed: {ex.Message}");
}
catch (TaskCanceledException)
{
    Console.WriteLine("API request timed out.");
}
catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
{
    Console.WriteLine($"Database update failed: {ex.Message}");
}
catch (Microsoft.Data.Sqlite.SqliteException ex)
{
    Console.WriteLine($"SQLite error: {ex.Message}");
}
catch (IOException ex)
{
    Console.WriteLine($"File operation failed: {ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"Unexpected error: {ex.Message}");
}