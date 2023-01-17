using Hangfire;
using Microsoft.AspNetCore.SignalR;
using BlazorLaboratory.WebApi.Helpers;

namespace BlazorLaboratory.WebApi.Hubs;

public class CounterHub : Hub
{
    public async Task AddToTotal(string user, int value)
    {
        await Clients.All.SendAsync("CounterIncrement", user, value);
        BackgroundJob.Schedule<CounterHubHelper>(h => h.ConfirmIncrementCounter(), TimeSpan.FromSeconds(3));
    }

}
