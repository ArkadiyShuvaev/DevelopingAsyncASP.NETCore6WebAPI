using System.Text.Json;
using Books.Api.Models;

namespace Books.Api.Services;

public class BookCoversProvider : IBookCoversProvider
{
    private readonly HttpClient _httpClient;

    public BookCoversProvider(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("BookCoversClient");
    }

    public async Task<BookCoverDto?> GetBookCoverAsync(int id)
    {
        var response = await _httpClient.GetAsync($"/api/bookcovers/{id}");
        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<BookCoverDto>(jsonString,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }

        return null;
    }
}
