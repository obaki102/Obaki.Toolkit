using System.Net.Http.Json;

namespace Obaki.Toolkit.Application.Features.D4Timer;

public class D4HttpClientWithProxy : ID4HttpClient
{
    private readonly HttpClient _httpClient;

    public D4HttpClientWithProxy(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(D4TimerConstants.BaseAddressWithProxy);
    }
    public async Task<D4Boss> GetUpcomingBoss()
    {
        
        var response = await _httpClient.GetAsync(D4TimerConstants.GetUpcomingBossWithProxy.Endpoint).ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Status code error: {response.StatusCode}");
        }

        var result = await response.Content.ReadFromJsonAsync<D4Boss>().ConfigureAwait(false);
        return result ?? throw new NullReferenceException("No value");
    }
}