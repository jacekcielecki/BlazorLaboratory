using Microsoft.JSInterop;

namespace BlazorLaboratory.BlazorServer.Pages;

public partial class CookiesManager
{
    private string _country;
    private string _newCookieValue = "";

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        _country = await GetCookie("country");
        if (_country == null)
        {
            Snackbar.Add("Country not set");
            _country = "";
        }
        StateHasChanged();
    }

    private async Task ShowAlert()
    {
        await JsRuntime.InvokeVoidAsync("blazorExtensions.createAlert");
    }

    private async Task SetCookie(string name, string value, int days)
    {
        await JsRuntime.InvokeVoidAsync("blazorExtensions.setCookie", name, value, days);
    }

    private async Task<string> GetCookie(string name)
    {
        var cookie = await JsRuntime.InvokeAsync<string>("blazorExtensions.getCookie", name);
        Snackbar.Add(cookie);
        return cookie;
    }
}
