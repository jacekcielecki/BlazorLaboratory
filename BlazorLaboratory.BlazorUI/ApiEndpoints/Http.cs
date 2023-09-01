using Newtonsoft.Json;
using System.Text;

namespace BlazorLaboratory.BlazorUI.ApiEndpoints;

public class Http : IHttp
{
    private readonly ILogger<Http> _logger;

    public Http(ILogger<Http> logger)
    {
        _logger = logger;
    }

    public async Task<IEnumerable<T>> GetListAsync<T>(IHttpClientFactory clientFactory, string path, string clientName)
    {
        var list = new List<T>();
        HttpClient client = clientFactory.CreateClient(clientName);
        try
        {
            using HttpResponseMessage response = await client.GetAsync(path);
            var responseAsString = await response.Content.ReadAsStringAsync();
            var deserializedResponseObject = JsonConvert.DeserializeObject<IEnumerable<T>>(responseAsString);
            if (deserializedResponseObject != null)
            {
                list.AddRange(deserializedResponseObject);
            }

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError(
                    "Error to Fetch data from {path} clientName {clientName} with statusCode {statusCode} and message {reason} token: {token}",
                    path, clientName, response.StatusCode, response.ReasonPhrase, client.DefaultRequestHeaders.Authorization?.Parameter);
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            throw;
        }

        return list;
    }

    public async Task<T> GetAsync<T>(IHttpClientFactory clientFactory, string path, string clientName)
    {
        HttpClient client = clientFactory.CreateClient(clientName);
        string? responseAsString;
        try
        {
            using HttpResponseMessage response = await client.GetAsync(path);
            responseAsString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError(
                    "Error to Fetch data from {path} clientName {clientName} with statusCode {statusCode} and message {reason} token: {token}",
                    path, clientName, response.StatusCode, response.ReasonPhrase, client.DefaultRequestHeaders.Authorization?.Parameter);
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            throw;
        }

        if (typeof(T) == typeof(string))
            return (T)(object)responseAsString;
        return JsonConvert.DeserializeObject<T>(responseAsString)!;
    }

    public async Task<bool> PostAsync<T>(IHttpClientFactory clientFactory, string path, T content,
        string clientName, IDictionary<string, string>? customHeaders = null)
    {
        HttpClient client = clientFactory.CreateClient(clientName);
        var json = JsonConvert.SerializeObject(content);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        if (customHeaders != null)
        {
            foreach (var header in customHeaders)
            {
                data.Headers.Add(header.Key, header.Value);
            }
        }

        try
        {
            using HttpResponseMessage response = await client.PostAsync(path, data);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError(
                    "Error to Fetch data from {path} clientName {clientName} with statusCode {statusCode} and message {reason} token: {token}",
                    path, clientName, response.StatusCode, response.ReasonPhrase, client.DefaultRequestHeaders.Authorization?.Parameter);
            }

            return response.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            throw;
        }
    }

    public async Task<bool> PostWithFormDataAsync(IHttpClientFactory clientFactory, string path,
        MultipartFormDataContent form, string clientName)
    {
        HttpClient client = clientFactory.CreateClient(clientName);
        try
        {
            using HttpResponseMessage response = await client.PostAsync(path, form);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError(
                    "Error to Fetch data from {path} clientName {clientName} with statusCode {statusCode} and message {reason} token: {token}",
                    path, clientName, response.StatusCode, response.ReasonPhrase, client.DefaultRequestHeaders.Authorization?.Parameter);
            }

            return response.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            throw;
        }
    }

    public async Task<T> PostAndReturnObjectAsync<T>(IHttpClientFactory clientFactory, string path, T content, string clientName)
    {
        HttpClient client = clientFactory.CreateClient(clientName);
        var json = JsonConvert.SerializeObject(content);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        string? responseAsString;
        try
        {
            using HttpResponseMessage response = await client.PostAsync(path, data);
            responseAsString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError(
                    "Error to Fetch data from {path} clientName {clientName} with statusCode {statusCode} and message {reason} token: {token}",
                    path, clientName, response.StatusCode, response.ReasonPhrase, client.DefaultRequestHeaders.Authorization?.Parameter);
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            throw;
        }

        return JsonConvert.DeserializeObject<T>(responseAsString)!;
    }

    public async Task<bool> PutAsync<T>(IHttpClientFactory clientFactory, string path, T content, string clientName)
    {
        HttpClient client = clientFactory.CreateClient(clientName);
        var json = JsonConvert.SerializeObject(content);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        try
        {
            using HttpResponseMessage response = await client.PutAsync(path, data);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError(
                    "Error to Fetch data from {path} clientName {clientName} with statusCode {statusCode} and message {reason} token: {token}",
                    path, clientName, response.StatusCode, response.ReasonPhrase, client.DefaultRequestHeaders.Authorization?.Parameter);
            }

            return response.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            throw;
        }
    }

    public async Task<bool> PatchAsync(IHttpClientFactory clientFactory, string path, string clientName)
    {
        HttpClient client = clientFactory.CreateClient(clientName);
        try
        {
            using HttpResponseMessage response = await client.PutAsync(path, null);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError(
                    "Error to Fetch data from {path} clientName {clientName} with statusCode {statusCode} and message {reason} token: {token}",
                    path, clientName, response.StatusCode, response.ReasonPhrase, client.DefaultRequestHeaders.Authorization?.Parameter);
            }

            return response.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            throw;
        }
    }

    public async Task<bool> DeleteAsync(IHttpClientFactory clientFactory, string path, string clientName,
        IDictionary<string, string>? customHeaders = null)
    {
        HttpClient client = clientFactory.CreateClient(clientName);
        var request = new HttpRequestMessage(HttpMethod.Delete, path);
        if (customHeaders != null)
        {
            foreach (var header in customHeaders)
            {
                request.Headers.Add(header.Key, header.Value);
            }
        }

        try
        {
            using HttpResponseMessage response = await client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError(
                    "Error to Fetch data from {path} clientName {clientName} with statusCode {statusCode} and message {reason} token: {token}",
                    path, clientName, response.StatusCode, response.ReasonPhrase, client.DefaultRequestHeaders.Authorization?.Parameter);
            }
            return response.IsSuccessStatusCode;

        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            throw;
        }
    }
}
