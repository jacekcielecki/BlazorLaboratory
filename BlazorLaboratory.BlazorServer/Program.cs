using BlazorLaboratory.BlazorServer.Circuit;
using BlazorLaboratory.BlazorServer.Data;
using BlazorLaboratory.BlazorServer.Hubs;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.AspNetCore.ResponseCompression;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddSingleton<ICircuitUserService, CircuitUserService>();
builder.Services.AddScoped<CircuitHandler>(sp => new CircuitHandlerService(sp.GetRequiredService<ICircuitUserService>()));

builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddMudServices();
builder.Services.AddControllers();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

RequestLocalizationOptions GetLocalizationOptions()
{
    var cultures = builder?.Configuration
        .GetSection("Cultures")
        .GetChildren()
        .ToDictionary(x => x.Key, x => x.Value);

    var supportedCultures = cultures?.Keys.ToArray();

    var localizationOptions = new RequestLocalizationOptions()
        .AddSupportedCultures(supportedCultures!)
        .AddSupportedUICultures(supportedCultures!);

    return localizationOptions;
}

builder.Services.AddResponseCompression(options =>
{
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

//builder.Services.AddHangfire(config => config
//    .UseSimpleAssemblyNameTypeSerializer()
//    .UseRecommendedSerializerSettings()
//    .UseSqlServerStorage(builder.Configuration.GetConnectionString("Default")));
//builder.Services.AddHangfireServer();
//test


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRequestLocalization(GetLocalizationOptions());

app.UseRouting();
app.UseCors("CorsPolicy");

app.MapControllers();
app.MapBlazorHub();
app.MapHub<ChatHub>("/chathub");
app.MapHub<CounterHub>("/counterhub");
app.MapFallbackToPage("/_Host");

app.Run();
