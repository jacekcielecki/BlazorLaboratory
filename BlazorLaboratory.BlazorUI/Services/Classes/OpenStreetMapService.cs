﻿using BlazorLaboratory.BlazorUI.Services.Interfaces;
using BlazorLaboratory.DataAccess.Models;
using Newtonsoft.Json;

namespace BlazorLaboratory.BlazorUI.Services.Classes;

public class OpenStreetMapService : IOpenStreetMapService
{
    public async Task<OSMGetCoordinatesResponse?> GetCoordinates(string country, string city, string street)
    {
        var address = $"{country} {city} {street}";
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("https://nominatim.openstreetmap.org");

        var response = await httpClient.GetAsync($"/search?format=json&q={address}");
        var responseAsString = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode && !string.IsNullOrEmpty(responseAsString))
        {
            var deserializedResponseObjects = JsonConvert.DeserializeObject<IEnumerable<OSMGetCoordinatesResponse>>(responseAsString);
            return deserializedResponseObjects?.First();
        }

        return null;
    }
}
