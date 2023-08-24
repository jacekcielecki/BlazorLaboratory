using System.Net.Http.Headers;
using BlazorLaboratory.BlazorUI.Services.Interfaces;

namespace BlazorLaboratory.BlazorUI.ApiEndpoints;

public class HttpRequestHandler : DelegatingHandler
{
    private readonly ILocalStorageService _localStorageService;

    public HttpRequestHandler(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        string? token = await _localStorageService.GetItem("blabToken");
        if (token != null)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        return await base.SendAsync(request, cancellationToken);
    }
}
