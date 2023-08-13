using BlazorLaboratory.BlazorUI;
using BlazorLaboratory.BlazorUI.ApiEndpoints;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Refit;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
var appConfig = builder.Configuration;
var graphUri = "https://localhost:7258/graphql";
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();
builder.Services.AddRefitClient<IUserClient>().ConfigureHttpClient(c => c.BaseAddress = new Uri(appConfig["ApiUri"]!));

builder.Services.AddGraphClient()
    .ConfigureHttpClient(client => client.BaseAddress = new Uri(graphUri));


await builder.Build().RunAsync();
