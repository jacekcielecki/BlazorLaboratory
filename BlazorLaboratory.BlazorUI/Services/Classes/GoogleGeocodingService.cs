using BlazorLaboratory.BlazorUI.Services.Interfaces;
using BlazorLaboratory.Shared.DTOs;
using Newtonsoft.Json;

namespace BlazorLaboratory.BlazorUI.Services.Classes;

public class GoogleGeocodingService : IGeocodingService
{
    private readonly ILogger<GoogleGeocodingService> _logger;
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly string _apiBaseUrl;

    public GoogleGeocodingService(IConfiguration configuration, ILogger<GoogleGeocodingService> logger)
    {
        _apiKey = configuration["GoogleMaps:ApiKeyBlazorLaboratory"];
        _apiBaseUrl = configuration["GoogleMaps:GeocodingApiUrl"];
        _httpClient = new HttpClient();
        _logger = logger;
    }

    public async Task<GetCoordinatesResponseDto?> GetCoordinates(AddressDto address)
    {
        try
        {
            string path = $"{_apiBaseUrl}/json?address={address.Country}+{address.City}+{address.Street}&key={_apiKey}";
            using HttpResponseMessage response = await _httpClient.GetAsync(path);

            var responseAsString = await response.Content.ReadAsStringAsync();
            var deserializedResponseObject = JsonConvert.DeserializeObject<GetCoordinatesResponseDto>(responseAsString);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError(
                    "Error to Fetch data from {path} clientName {clientName} with statusCode {statusCode} and message {reason}",
                    path, "Google geocoding api", response.StatusCode, response.ReasonPhrase);

                return null;
            }

            if (deserializedResponseObject?.Status != "OK" &&
                deserializedResponseObject?.Status != "ZERO_RESULTS")
            {
                _logger.LogError("Error to Fetch address coordinates from GoogleGeocoding Api with status: {status}", deserializedResponseObject?.Status);

                return null;
            }

            return deserializedResponseObject;
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return null;
        }
    }
}
