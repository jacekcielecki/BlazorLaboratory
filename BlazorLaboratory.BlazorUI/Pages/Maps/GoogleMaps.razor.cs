using Microsoft.JSInterop;
using MudBlazor;

namespace BlazorLaboratory.BlazorUI.Pages.Maps;

public partial class GoogleMaps
{
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await JsRuntime.InvokeVoidAsync("googleMaps.initMap");
        }
    }

    private void ShowMarkers()
    {
        Snackbar.Add("This is working!", Severity.Success);
    }
}

