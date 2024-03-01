using Microsoft.JSInterop;

namespace BlazorLaboratory.BlazorUI.Pages.DataSources;

public partial class CookiesManager
{
    private string? _country;
    private string _newCookieValue = "";

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _country = await GetCookie("country");
            if (_country == null)
            {
                Snackbar.Add("Country not set");
                _country = "";
            }
            StateHasChanged();
        }
    }

    private async Task ShowAlert()
    {
        await JsRuntime.InvokeVoidAsync("blazorExtensions.createAlert");
    }

    private async Task SetCookie(string name, string value, int days)
    {
        await JsRuntime.InvokeVoidAsync("blazorExtensions.setCookie", name, value, days);
        StateHasChanged();
    }

    private async Task<string?> GetCookie(string name)
    {
        var cookie = await JsRuntime.InvokeAsync<string?>("blazorExtensions.getCookie", name);
        if (cookie != null)
        {
            Snackbar.Add(cookie);
            StateHasChanged();
            return cookie;
        }
        return null;
    }

    private async Task DeleteCookie(string name)
    {
        await JsRuntime.InvokeVoidAsync("blazorExtensions.deleteCookie", name);
        Snackbar.Add("cookie has been removed!");
        StateHasChanged();
    }
}
