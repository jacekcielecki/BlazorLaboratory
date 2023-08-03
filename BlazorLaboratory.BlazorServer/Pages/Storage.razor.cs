using Microsoft.JSInterop;
using MudBlazor;

namespace BlazorLaboratory.BlazorServer.Pages;

public partial class Storage
{
    private string? _country;
    private string _newItemValue = "";

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _country = await GetItem("country") ?? "";
            StateHasChanged();
        }
    }

    private async Task ShowAlert()
    {
        await JsRuntime.InvokeVoidAsync("showAlert", null);
    }

    private async Task SetItem(string key, string value)
    {
        await ProtectedLocalStorage.SetAsync(key, value);
        Snackbar.Add($"New {key} has been set", Severity.Info);
        StateHasChanged();
    }

    private async Task<string?> GetItem(string key)
    {
        var result = await ProtectedLocalStorage.GetAsync<string>(key);
        var message = result.Success ? $"Current value of {key} is equal to: {result.Value}" : $"Currently value of {key} is not set";
        Snackbar.Add(message, Severity.Info);
        StateHasChanged();
        return result.Value;
    }

    private async Task DeleteItem(string key)
    {
        await ProtectedLocalStorage.DeleteAsync(key);
        Snackbar.Add($"{key} has been removed!", Severity.Info);
        StateHasChanged();
    }
}
