using BlazorLaboratory.BlazorUI;
using BlazorLaboratory.BlazorUI.ApiEndpoints;
using BlazorLaboratory.BlazorUI.Services.Classes;
using BlazorLaboratory.BlazorUI.Services.Interfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Refit;
using System.Net.Http.Headers;

var token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjYzODBlZjEyZjk1ZjkxNmNhZDdhNGNlMzg4ZDJjMmMzYzIzMDJmZGUiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL3NlY3VyZXRva2VuLmdvb2dsZS5jb20vYmxhem9ybGFib3JhdG9yeWdyYXBoIiwiYXVkIjoiYmxhem9ybGFib3JhdG9yeWdyYXBoIiwiYXV0aF90aW1lIjoxNjkyNTQ1NTYyLCJ1c2VyX2lkIjoiUUlpaGZiRU80bWJpeVFsQzdoQUQ1M2IwZEh0MiIsInN1YiI6IlFJaWhmYkVPNG1iaXlRbEM3aEFENTNiMGRIdDIiLCJpYXQiOjE2OTI1NDU1NjIsImV4cCI6MTY5MjU0OTE2MiwiZW1haWwiOiJqYWNlazkwMEBnbWFpbC5jb20iLCJlbWFpbF92ZXJpZmllZCI6ZmFsc2UsImZpcmViYXNlIjp7ImlkZW50aXRpZXMiOnsiZW1haWwiOlsiamFjZWs5MDBAZ21haWwuY29tIl19LCJzaWduX2luX3Byb3ZpZGVyIjoicGFzc3dvcmQifX0.DEbZd5CwdyUAwYGXaIpz5GPl8dDLHL9rIhdELpeCNQpykuQZrW4M1eKU0LY5Im8KsZ7qsKmtE7d0GLrQmVBlsl8evcG1WmmwpKQwFJ4XryYl4LWFpQgDDcjJPfMnczTATm5bormWphhiwya0zIN4IOPfUviFNmFT1Q8Im7fx_VdQtH5rLLxH-__YzAsPud0h7elbC00ZUDWgzcWGGXK5sh_PfH_M8J4bdWE1k2RH-WSc98WRLZeAKCLNcW2E5VCouPH4qWwgTROgRokWHIKbcZs1sajHKiCTJbc8Elb83OwXmhwQVMCtS9JGWRFTolkWw7rRJ5JOGIvoq_8UmOp-6A";

var builder = WebAssemblyHostBuilder.CreateDefault(args);
var appConfig = builder.Configuration;
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<IAuthorizationManager, AuthorizationManager>();
builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
builder.Services.AddMudServices();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddGraphClient()
    .ConfigureHttpClient(client =>
    {
        client.BaseAddress = new Uri(appConfig["GraphUri"]!);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    })
    .ConfigureWebSocketClient(client => client.Uri = new Uri(appConfig["GraphWebSocketUri"]!));
builder.Services.AddRefitClient<IUserClient>().ConfigureHttpClient(httpClient =>
{
    httpClient.BaseAddress = new Uri(appConfig["ApiUri"]!);
    httpClient.AddDefaultSecurityHeaders();
});


await builder.Build().RunAsync();
