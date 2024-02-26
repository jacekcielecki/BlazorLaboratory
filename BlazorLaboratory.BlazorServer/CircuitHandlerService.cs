using Microsoft.AspNetCore.Components.Server.Circuits;

namespace BlazorLaboratory.BlazorServer;

public class CircuitHandlerService : CircuitHandler
{
    public override Task OnCircuitOpenedAsync(Circuit circuit, CancellationToken cancellationToken)
    {
        var circuitId = circuit.Id;
        return base.OnCircuitOpenedAsync(circuit, cancellationToken);
    }

    public override Task OnCircuitClosedAsync(Circuit circuit, CancellationToken cancellationToken)
    {
        var circuitId = circuit.Id;
        return base.OnCircuitClosedAsync(circuit, cancellationToken);
    }
}
