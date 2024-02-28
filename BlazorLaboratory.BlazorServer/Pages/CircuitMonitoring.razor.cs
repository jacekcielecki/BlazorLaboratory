using BlazorLaboratory.BlazorServer.Circuit;
using Microsoft.AspNetCore.Components;

namespace BlazorLaboratory.BlazorServer.Pages;

public partial class CircuitMonitoring : ComponentBase, IDisposable
{
    public string MyCircuitMessage = "";
    public string MyUserId = "";

    private CircuitHandlerService _handler;

    protected override void OnInitialized()
    {
        _handler = (CircuitHandlerService)CircuitHandler;
        MyCircuitMessage = $"My Circuit ID = {_handler.CircuitId}";

        MyUserId = CircuitUserService.Circuits.Count switch
        {
            0 => "John",
            1 => "Kelly",
            >= 2 => "Alan",
            _ => MyUserId
        };

        CircuitUserService.Connect(_handler.CircuitId, MyUserId);
        CircuitUserService.CircuitsChanged += CircuitUserService_CircuitsChanged;
    }

    private void CircuitUserService_CircuitsChanged(object sender, EventArgs e)
    {
        InvokeAsync(StateHasChanged);
    }

    public void Dispose()
    {
        CircuitUserService.CircuitsChanged -= CircuitUserService_CircuitsChanged;
    }
}
