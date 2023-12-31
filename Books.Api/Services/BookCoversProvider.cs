using System.Text.Json;
using Books.Api.Models.External;

namespace Books.Api.Services;

public class BookCoversProvider : IBookCoversProvider
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<BookCoversProvider> _logger;

    public BookCoversProvider(IHttpClientFactory httpClientFactory, ILogger<BookCoversProvider> logger)
    {
        _httpClient = httpClientFactory.CreateClient("BookCoversClient");
        _logger = logger;
    }

    public async Task<BookCoverResponse?> GetBookCoverAsync(int id)
    {
        var response = await _httpClient.GetAsync($"/api/bookcovers/{id}");
        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<BookCoverResponse>(jsonString,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }

        return null;
    }

    public async Task<IEnumerable<BookCoverResponse>> GetBookCoversProcessOneByOneAsync(IEnumerable<int> bookIds,
                                                                                        CancellationToken ct)
    {
        var requestUris = new List<string>()
        {
            "/api/bookcovers/1",
            "/api/bookcovers/2",
            //"/api/bookcovers/3?returnFault=true", // this will return a 500
            "/api/bookcovers/4"
        };

        using var cts = new CancellationTokenSource();
        using var lcts = CancellationTokenSource.CreateLinkedTokenSource(cts.Token, ct);

        var bookCovers = new List<BookCoverResponse>();

        var jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        foreach (var requestUri in requestUris)
        {
            _logger.LogInformation("Getting book cover from: {RequestUri}.", requestUri);

            var response = await _httpClient.GetAsync(requestUri, lcts.Token);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync(lcts.Token);
                var bookCover = JsonSerializer.Deserialize<BookCoverResponse>(jsonString, jsonSerializerOptions);

                if (bookCover != null)
                {
                    bookCovers.Add(bookCover);
                }

                // to see the effect
                await Task.Delay(500);

                continue;
            }

            lcts.Cancel();
        }

        return bookCovers;
    }
}
