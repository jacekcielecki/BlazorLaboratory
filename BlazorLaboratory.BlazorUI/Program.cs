using BlazorLaboratory.BlazorUI;
using BlazorLaboratory.BlazorUI.ApiEndpoints;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Refit;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
var appConfig = builder.Configuration;
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();
builder.Services.AddRefitClient<IUserClient>().ConfigureHttpClient(httpClient =>
{
    httpClient.BaseAddress = new Uri(appConfig["ApiUri"]!);
    httpClient.AddDefaultSecurityHeaders();
});

await builder.Build().RunAsync();
