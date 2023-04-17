using BlazorLaboratory.DataAccess.Data;
using BlazorLaboratory.DataAccess.Repositories;
using BlazorLaboratory.WebApi;
using BlazorLaboratory.WebApi.Helpers;
using BlazorLaboratory.WebApi.Hubs;
using Hangfire;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddResponseCompression(options =>
{
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});
builder.Services.AddCors(policy =>
{
    policy.AddPolicy("OpenCorsPolicy", opt =>
        opt.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});
builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddSingleton<IUserData, UserData>();
builder.Services.AddScoped<IUserDataRepository, UserDataRepository>();
builder.Services.AddScoped<IUserGroupRepository, UserGroupRepository>();
builder.Services.AddSingleton<ICounterHubHelper, CounterHubHelper>();

builder.Services.AddHangfire(config => config
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddHangfireServer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseHangfireDashboard();
app.MapHangfireDashboard();
//RecurringJob.AddOrUpdate<TestService>(x => x.SendMessage(), Cron.Minutely);
app.MapHub<CounterHub>("/counterhub");
app.MapHub<ChatHub>("/chathub");
app.UseCors("OpenCorsPolicy");
app.ConfigureApi();

app.Run();
