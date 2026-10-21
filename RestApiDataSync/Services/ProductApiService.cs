using System.Net.Http.Json;
using RestApiDataSync.Models;

namespace RestApiDataSync.Services;

public class ProductApiService
{
    private readonly HttpClient _httpClient;

    public ProductApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        const string url = "https://fakestoreapi.com/products";

        List<Product>? products =
            await _httpClient.GetFromJsonAsync<List<Product>>(url);

        return products ?? new List<Product>();
    }
}