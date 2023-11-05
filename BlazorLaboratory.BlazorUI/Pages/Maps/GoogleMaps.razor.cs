using BlazorLaboratory.Shared.DTOs;
using Microsoft.JSInterop;
using MudBlazor;

namespace BlazorLaboratory.BlazorUI.Pages.Maps;

public partial class GoogleMaps
{
    private List<AddressDto> _projectAddressList = new();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await JsRuntime.InvokeVoidAsync("googleMaps.initMap");
            await LoadDefaultMarkers();
            _projectAddressList = GetProjectAddresses();
        }
    }

    private async Task LoadDefaultMarkers()
    {
        await JsRuntime.InvokeVoidAsync("googleMaps.loadDefaultMarker");
    }

    private async Task GeoCodeAddress(string address)
    {
        var location = await JsRuntime.InvokeAsync<string>("googleMaps.geocodeAddress", address);
        Snackbar.Add(location, Severity.Success);
    }

    private List<AddressDto> GetProjectAddresses()
    {
        return new List<AddressDto>
        {
            new() {Country = "Poland", City = "Poznań", Street = "Głogowska 4", ZipCode = "60-266"},
            new() {Country = "Germany", City = "Berlin", Street = "Pariser Platz", ZipCode = "10117"},
            new() {Country = "Italy", City = "Roma", Street = "Via Montebello 126", ZipCode = "00185"},
            new() {Country = "France", City = "Paris", Street = "4 Rue Niépce", ZipCode = "75014"},
        };
    }
}
