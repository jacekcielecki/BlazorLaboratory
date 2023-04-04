using BlazorLaboratory.BlazorServer.Helpers;
using Hangfire;
using Microsoft.AspNetCore.SignalR;


namespace BlazorLaboratory.BlazorServer.Hubs;

public class CounterHub : Hub
{
    public async Task AddToTotal(string user, int value)
    {
        await Clients.All.SendAsync("CounterIncrement", user, value);
        BackgroundJob.Schedule<CounterHubHelper>(h => h.ConfirmIncrementCounter(), TimeSpan.FromSeconds(3));
    }

}
