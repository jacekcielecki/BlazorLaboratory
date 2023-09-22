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
var apiUri = builder.Configuration.GetValue<string>("ApiUri");
var graphUri = builder.Configuration.GetValue<string>("GraphUri");
var graphWebSocketUri = builder.Configuration.GetValue<string>("GraphWebSocketUri");

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton<FirebaseToken>();
builder.Services.AddScoped<IAuthorizationManager, AuthorizationManager>();
builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
builder.Services.AddScoped<IHttp, Http>();
builder.Services.AddMudServices();
builder.Services.AddTransient<HttpRequestHandler>();

builder.Services.AddGraphClient()
    .ConfigureHttpClient((services, client) =>
    {
        FirebaseToken token = services.GetRequiredService<FirebaseToken>();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);
        client.BaseAddress = new Uri(graphUri);
    })
    .ConfigureWebSocketClient(client => client.Uri = new Uri(graphWebSocketUri));

builder.Services.AddRefitClient<IUserClient>()
    .ConfigureHttpClient(httpClient =>
    {
        httpClient.BaseAddress = new Uri(apiUri);
        httpClient.AddDefaultSecurityHeaders();
    }).AddHttpMessageHandler<HttpRequestHandler>();


await builder.Build().RunAsync();
//cherry pick
