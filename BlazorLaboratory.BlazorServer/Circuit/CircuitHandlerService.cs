using Microsoft.AspNetCore.Components.Server.Circuits;

namespace BlazorLaboratory.BlazorServer.Circuit;

public class CircuitHandlerService : CircuitHandler
{
    private readonly ICircuitUserService _userService;
    public string CircuitId { get; private set; } = "";

    public CircuitHandlerService(ICircuitUserService userService)
    {
        _userService = userService;
    }

    public override Task OnCircuitOpenedAsync(Microsoft.AspNetCore.Components.Server.Circuits.Circuit circuit, CancellationToken cancellationToken)
    {
        CircuitId = circuit.Id;
        return base.OnCircuitOpenedAsync(circuit, cancellationToken);
    }

    public override Task OnCircuitClosedAsync(Microsoft.AspNetCore.Components.Server.Circuits.Circuit circuit, CancellationToken cancellationToken)
    {
        _userService.Disconnect(CircuitId);
        return base.OnCircuitClosedAsync(circuit, cancellationToken);
    }
}
