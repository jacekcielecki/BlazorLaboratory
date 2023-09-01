namespace BlazorLaboratory.BlazorUI.ApiEndpoints;

public interface IHttp
{
    public Task<IEnumerable<T>> GetListAsync<T>(IHttpClientFactory clientFactory, string path, string clientName);
    public Task<T> GetAsync<T>(IHttpClientFactory clientFactory, string path, string clientName);
    public Task<bool> PostAsync<T>(IHttpClientFactory clientFactory, string path, T content,
        string clientName, IDictionary<string, string>? customHeaders = null);
    public Task<bool> PostWithFormDataAsync(IHttpClientFactory clientFactory, string path,
        MultipartFormDataContent form, string clientName);
    public Task<T> PostAndReturnObjectAsync<T>(IHttpClientFactory clientFactory, string path, T content, string clientName);
    public Task<bool> PutAsync<T>(IHttpClientFactory clientFactory, string path, T content, string clientName);
    public Task<bool> PatchAsync(IHttpClientFactory clientFactory, string path, string clientName);
    public Task<bool> DeleteAsync(IHttpClientFactory clientFactory, string path, string clientName,
        IDictionary<string, string>? customHeaders = null);
}

