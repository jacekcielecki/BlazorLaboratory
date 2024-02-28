using System.Collections.Concurrent;

namespace BlazorLaboratory.BlazorServer.Circuit;

public class CircuitUserService : ICircuitUserService
{
    public ConcurrentDictionary<string, CircuitUser> Circuits { get; private set; } = new();
    public event EventHandler? CircuitsChanged;

    void OnCircuitsChanged() => CircuitsChanged?.Invoke(this, EventArgs.Empty);

    public void Connect(string circuitId, string userId)
    {
        if (Circuits.ContainsKey(circuitId))
        {
            Circuits[circuitId].UserId = userId;
        }
        else
        {
            var circuitUser = new CircuitUser
            {
                UserId = userId,
                CircuitId = circuitId,
            };
            Circuits[circuitId] = circuitUser;
        }
        OnCircuitsChanged();
    }

    public void Disconnect(string circuitId)
    {
        CircuitUser circuitRemoved;
        Circuits.TryRemove(circuitId, out circuitRemoved);
        if (circuitRemoved != null)
        {
            OnCircuitsChanged();
        }
    }
}
