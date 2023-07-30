using BlazorLaboratory.GraphQL.DataLoaders;
using BlazorLaboratory.GraphQL.Schema.Mutations;
using BlazorLaboratory.GraphQL.Schema.Queries;
using BlazorLaboratory.GraphQL.Schema.Subscriptions;
using BlazorLaboratory.GraphQL.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("SchoolDb");

builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>()
    .AddInMemorySubscriptions()
    .AddFiltering();

builder.Services.AddPooledDbContextFactory<SchoolDbContext>(x => 
    x.UseSqlServer(connectionString)).AddLogging(x => x.AddConsole());
builder.Services.AddScoped<CoursesRepository>();
builder.Services.AddScoped<InstructorRepository>();
builder.Services.AddScoped<InstructorDataLoader>();

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
app.UseEndpoints(endpoints =>
{ 
    endpoints.MapGraphQL();
});

//app.MapGet("/", () => "Hello World!");

app.Run();
