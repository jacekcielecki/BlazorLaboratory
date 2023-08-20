using BlazorLaboratory.BlazorUI;
using BlazorLaboratory.BlazorUI.ApiEndpoints;
using BlazorLaboratory.BlazorUI.Services.Classes;
using BlazorLaboratory.BlazorUI.Services.Interfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Refit;
using System.Net.Http.Headers;

var token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjYzODBlZjEyZjk1ZjkxNmNhZDdhNGNlMzg4ZDJjMmMzYzIzMDJmZGUiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL3NlY3VyZXRva2VuLmdvb2dsZS5jb20vYmxhem9ybGFib3JhdG9yeWdyYXBoIiwiYXVkIjoiYmxhem9ybGFib3JhdG9yeWdyYXBoIiwiYXV0aF90aW1lIjoxNjkyNTUyMzg4LCJ1c2VyX2lkIjoiUUlpaGZiRU80bWJpeVFsQzdoQUQ1M2IwZEh0MiIsInN1YiI6IlFJaWhmYkVPNG1iaXlRbEM3aEFENTNiMGRIdDIiLCJpYXQiOjE2OTI1NTIzODgsImV4cCI6MTY5MjU1NTk4OCwiZW1haWwiOiJqYWNlazkwMEBnbWFpbC5jb20iLCJlbWFpbF92ZXJpZmllZCI6ZmFsc2UsImZpcmViYXNlIjp7ImlkZW50aXRpZXMiOnsiZW1haWwiOlsiamFjZWs5MDBAZ21haWwuY29tIl19LCJzaWduX2luX3Byb3ZpZGVyIjoicGFzc3dvcmQifX0.Edj1wXuyBMQBOBZeq_pkQWjGXlnQdon6xfDvYrsN0XPadaV5tdDStu4xjiCf7zPhHm_623kVRh_BMjJ_pfZwmW9gXVznOf4RaL7hS0s4Gp0a__IsSmrc9YgXRjTzHQUaiSg7dX0XdFcdUlDqPMdHHsEbpZXyP-hX5rHTeMcZt0pCEldVHpsLa0-8ockmPPmUgpuYKpA4eSvxSzIWNL62OXIUdQVFQ2eyRoCmb4QoF42ucKtmQ2Xnf7IXBpTUWCZRiguAwPj900ELvcHmqyezTrEcnQ5xB_EcDPbVqK-NvdAayHqAjH9PwQMpF-vQN2dL6js7H152Xd7RfgT3bfgNHw";

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
