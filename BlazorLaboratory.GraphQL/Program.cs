using AppAny.HotChocolate.FluentValidation;
using BlazorLaboratory.GraphQL.DataLoaders;
using BlazorLaboratory.GraphQL.Schema.Mutations;
using BlazorLaboratory.GraphQL.Schema.Queries;
using BlazorLaboratory.GraphQL.Schema.Queries.Course;
using BlazorLaboratory.GraphQL.Schema.Queries.Instructor;
using BlazorLaboratory.GraphQL.Schema.Subscriptions;
using BlazorLaboratory.GraphQL.Services;
using BlazorLaboratory.GraphQL.Validators;
using FirebaseAdmin;
using FirebaseAdminAuthentication.DependencyInjection.Extensions;
using FirebaseAdminAuthentication.DependencyInjection.Models;
using FluentValidation.AspNetCore;
using Google.Apis.Auth.OAuth2;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("SchoolDb");

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<CourseTypeInputValidator>();

builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>()
    .AddType<CourseType>()
    .AddTypeExtension<CourseQuery>()
    .AddType<InstructorType>()
    .AddTypeExtension<InstructorQuery>()
    .AddInMemorySubscriptions()
    .AddFiltering()
    .AddSorting()
    .AddProjections()
    .AddAuthorization()
    .AddFluentValidation();

builder.Services.AddSingleton(FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile(builder.Configuration.GetValue<string>("FIREBASE_CONFIG"))
}));
builder.Services.AddFirebaseAuthentication();
builder.Services.AddAuthorization(
    x => x.AddPolicy("IsAdmin",
        p => p.RequireClaim(FirebaseUserClaimType.EMAIL, "jacek900@gmail.com")));

builder.Services.AddPooledDbContextFactory<SchoolDbContext>(x => x
    .UseSqlServer(connectionString))
    .AddLogging(x => x.AddConsole());
builder.Services.AddScoped<CoursesRepository>();
builder.Services.AddScoped<InstructorRepository>();
builder.Services.AddScoped<InstructorDataLoader>();
builder.Services.AddScoped<UserDataLoader>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    IDbContextFactory<SchoolDbContext> contextFactory =
        scope.ServiceProvider.GetRequiredService<IDbContextFactory<SchoolDbContext>>();

    using (SchoolDbContext context = contextFactory.CreateDbContext())
    {
        var pendingMigrations = context.Database.GetPendingMigrations();
        if (pendingMigrations.Any())
        {
            context.Database.Migrate();
        }
    }
}

app.UseRouting();
app.UseWebSockets();
app.UseAuthentication();
app.UseEndpoints(endpoints =>
{ 
    endpoints.MapGraphQL();
});

app.MapGet("/", () => "Hello World!");

app.Run();
