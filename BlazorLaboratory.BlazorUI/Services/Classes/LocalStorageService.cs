using BlazorLaboratory.BlazorUI.Services.Interfaces;
using Microsoft.JSInterop;

namespace BlazorLaboratory.BlazorUI.Services.Classes;

public class LocalStorageService : ILocalStorageService
{
    private readonly IJSRuntime _jSRuntime;

    public LocalStorageService(IJSRuntime jSRuntime)
    {
        _jSRuntime = jSRuntime;
    }

    public async Task SetItem(string key, string value)
    {
        await _jSRuntime.InvokeVoidAsync("localStorageInterop.setItem", key, value);
    }

    public async Task<string?> GetItem(string key)
    {
        return await _jSRuntime.InvokeAsync<string?>("localStorageInterop.getItem", key);
    }

    public async Task RemoveItem(string key)
    {
        await _jSRuntime.InvokeVoidAsync("localStorageInterop.removeItem", key);
    }
}
