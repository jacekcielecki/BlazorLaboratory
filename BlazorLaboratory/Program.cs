using BlazorLaboratory.BlazorUI;
using BlazorLaboratory.BlazorUI.ApiEndpoints;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Refit;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();
//var apiUrl = builder.Configuration["APIUrl"];
//if (apiUrl is null)
//{
//    return;
//}

builder.Services.AddRefitClient<IUserClient>().ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7234"));



await builder.Build().RunAsync();
