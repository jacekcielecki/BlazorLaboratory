using BlazorLaboratory.BlazorUI;
using BlazorLaboratory.BlazorUI.ApiEndpoints;
using BlazorLaboratory.BlazorUI.Services.Classes;
using BlazorLaboratory.BlazorUI.Services.Interfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Refit;
using System.Net.Http.Headers;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
var appConfig = builder.Configuration;
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton<FirebaseToken>();
builder.Services.AddScoped<IAuthorizationManager, AuthorizationManager>();
builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
builder.Services.AddMudServices();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddGraphClient()
    .ConfigureHttpClient((services, client) =>
    {
        FirebaseToken token = services.GetRequiredService<FirebaseToken>();
        client.BaseAddress = new Uri(appConfig["GraphUri"]!);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);
    })
    .ConfigureWebSocketClient(client => client.Uri = new Uri(appConfig["GraphWebSocketUri"]!));
builder.Services.AddRefitClient<IUserClient>().ConfigureHttpClient(httpClient =>
{
    httpClient.BaseAddress = new Uri(appConfig["ApiUri"]!);
    httpClient.AddDefaultSecurityHeaders();
});


await builder.Build().RunAsync();
