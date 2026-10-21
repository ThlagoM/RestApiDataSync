using System.Text.Json.Serialization;

namespace RestApiDataSync.Models;

public class Product
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public decimal Price { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;
}