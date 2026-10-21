using Microsoft.EntityFrameworkCore;
using RestApiDataSync.Data;
using RestApiDataSync.Models;

namespace RestApiDataSync.Services;

public class ProductSyncService
{
    private readonly AppDbContext _db;

    public ProductSyncService(AppDbContext db)
    {
        _db = db;
    }

    public async Task SyncAsync(List<Product> products)
    {
        int inserted = 0;
        int updated = 0;
        int unchanged = 0;
        foreach (Product product in products)
        {
            Product? existing = await _db.Products
                .FirstOrDefaultAsync(p => p.Id == product.Id);

            if (existing == null)
            {
                _db.Products.Add(product);
                inserted++;
            }
            else
            {
                bool hasChanges =
                    existing.Title != product.Title ||
                    existing.Price != product.Price ||
                    existing.Description != product.Description ||
                    existing.Category != product.Category;

                if (hasChanges)
                {
                    existing.Title = product.Title;
                    existing.Price = product.Price;
                    existing.Description = product.Description;
                    existing.Category = product.Category;

                    updated++;
                }
                else
                {
                    unchanged++;
                }
            }
        }

        await _db.SaveChangesAsync();

        Console.WriteLine($"Products inserted: {inserted}");
        Console.WriteLine($"Products updated: {updated}");
        Console.WriteLine($"Products unchanged: {unchanged}");

        string logMessage =
    $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} UTC | " +
    $"Received: {products.Count} | " +
    $"Inserted: {inserted} | " +
    $"Updated: {updated} | " +
    $"Unchanged: {unchanged}";

        await File.AppendAllTextAsync(
            "sync.log",
            logMessage + Environment.NewLine);

        Console.WriteLine("Synchronization logged successfully.");
    }
}