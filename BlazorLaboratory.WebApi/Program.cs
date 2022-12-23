using BlazorLaboratory.DataAccess.Data;
using BlazorLaboratory.WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(policy =>
{
    policy.AddPolicy("OpenCorsPolicy", opt =>
        opt.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});
builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddSingleton<IUserData, UserData>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("OpenCorsPolicy");
app.ConfigureApi();

app.Run();
