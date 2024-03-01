using BlazorLaboratory.BlazorServer.Circuit;
using Microsoft.AspNetCore.Components;

namespace BlazorLaboratory.BlazorServer.Pages;

public partial class CircuitMonitoring : ComponentBase, IDisposable
{
    public string MyCircuitMessage = "";
    public string UserName = "";

    private CircuitHandlerService _handler;

    protected override void OnInitialized()
    {
        _handler = (CircuitHandlerService)CircuitHandler;
        MyCircuitMessage = $"My Circuit ID = {_handler.CircuitId}";

        UserName = CircuitUserService.Circuits.Count switch
        {
            0 => "John",
            1 => "Kelly",
            2 => "Alan",
            3 => "Luke",
            _ => "Some other user"
        };

        CircuitUserService.Connect(_handler.CircuitId, UserName);
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
