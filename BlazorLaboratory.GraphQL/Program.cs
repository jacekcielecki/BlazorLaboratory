using BlazorLaboratory.GraphQL.Schema;
using BlazorLaboratory.GraphQL.Schema.Mutations;
using BlazorLaboratory.GraphQL.Schema.Subscriptions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>()
    .AddInMemorySubscriptions(); ;

var app = builder.Build();

app.UseRouting();
app.UseWebSockets();
app.UseEndpoints(endpoints =>
{ 
    endpoints.MapGraphQL();
});

//app.MapGet("/", () => "Hello World!");

app.Run();
