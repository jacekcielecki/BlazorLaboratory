using BlazorLaboratory.WebApi.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace BlazorLaboratory.WebApi.Helpers;

public class CounterHubHelper : ICounterHubHelper
{
    private readonly IHubContext<CounterHub> _hubContext;

    public CounterHubHelper(IHubContext<CounterHub> hubContext)
    {
        _hubContext = hubContext;
    }


    public void ConfirmIncrementCounter()
    {
        _hubContext.Clients.All.SendAsync("CounterIncrementConfirmation");
    }
}