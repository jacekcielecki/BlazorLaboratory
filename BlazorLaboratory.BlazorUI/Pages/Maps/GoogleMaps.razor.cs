using BlazorLaboratory.Shared.DTOs;
using Microsoft.JSInterop;
using MudBlazor;

namespace BlazorLaboratory.BlazorUI.Pages.Maps;

public partial class GoogleMaps
{
    private List<GetCoordinatesResponseDto> _coordinates = new();
    private List<AddressDto> _projectAddressList = new()
    {
        new () {Country = "Poland", City = "Poznań", Street = "Głogowska", ZipCode = "60-266"},
        new () {Country = "Germany", City = "Berlin", Street = "Pariser Platz", ZipCode = "10117"},
        new() { Country = "Italy", City = "Roma", Street = "Via Montebello", ZipCode = "00185" },
        new() { Country = "France", City = "Paris", Street = "Rue Niépce", ZipCode = "75014" },
    };

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await JsRuntime.InvokeVoidAsync("googleMaps.initMap");
            await LoadDefaultMarkers();
            await GetAddressCoordinates();
        }
    }

    private async Task LoadDefaultMarkers()
    {
        await JsRuntime.InvokeVoidAsync("googleMaps.loadDefaultMarkers");
    }

    private async Task AddMarker(double latitude, double longitude, string name)
    {
        await JsRuntime.InvokeVoidAsync("googleMaps.addMarker", latitude, longitude, name);
    }

    private async Task GetAddressCoordinates()
    {
        foreach (var address in _projectAddressList)
        {
            var coordinates = await GeocodingService.GetCoordinates(address);
            if (coordinates != null)
            {
                double? lat = coordinates.Results.FirstOrDefault()?.Geometry.Location.Lat;
                double? lng = coordinates.Results.FirstOrDefault()?.Geometry.Location.Lng;
                if (lat != null & lng != null)
                {
                    _coordinates.Add(coordinates);
                    await AddMarker((double)lat!, (double)lng!, "New BLAB project");
                    Snackbar.Add($"{lat}, " + $"{lng}", Severity.Success);
                }
            }
            else
            {
                Snackbar.Add($"Unable to get coordinates for {address.Country} {address.City}", Severity.Error);
            }
        }
    }
}
