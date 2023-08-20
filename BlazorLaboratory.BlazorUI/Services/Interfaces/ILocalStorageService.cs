namespace BlazorLaboratory.BlazorUI.Services.Interfaces;

public interface ILocalStorageService
{
    public Task SetItem(string key, string value);
    public Task<string> GetItem(string key);
    public Task RemoveItem(string key);
}
