using BlazorLaboratory.GraphQL.Schema;
using BlazorLaboratory.GraphQL.Schema.Mutations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>();

var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints =>
{ 
    endpoints.MapGraphQL();
});

//app.MapGet("/", () => "Hello World!");

app.Run();
