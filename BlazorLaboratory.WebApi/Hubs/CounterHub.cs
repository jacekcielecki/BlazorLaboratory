using Microsoft.AspNetCore.SignalR;

namespace BlazorLaboratory.WebApi.Hubs;

public class CounterHub : Hub
{
    public Task AddToTotal(string user, int value)
    {
        return Clients.All.SendAsync("CounterIncrement", user, value);
    }
}
