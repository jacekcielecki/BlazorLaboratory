using Microsoft.JSInterop;
using MudBlazor;

namespace BlazorLaboratory.BlazorUI.Pages.Maps;

public partial class GoogleMaps
{
    private string _inputAddress;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await JsRuntime.InvokeVoidAsync("googleMaps.initMap");
        }
    }

    private async Task ShowMarker(string location)
    {
        await JsRuntime.InvokeVoidAsync("googleMaps.addMarker", location);
    }

    private async Task LoadDefaultMarkers()
    {
        await JsRuntime.InvokeVoidAsync("googleMaps.loadDefaultMarker");
    }

    private async Task GeoCodeAddress()
    {
        var location = await JsRuntime.InvokeAsync<string>("googleMaps.geocodeAddress", _inputAddress);
        Snackbar.Add(location, Severity.Success);
    }
}

