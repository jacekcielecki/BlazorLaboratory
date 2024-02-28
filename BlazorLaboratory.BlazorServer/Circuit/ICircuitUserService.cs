using System.Collections.Concurrent;

namespace BlazorLaboratory.BlazorServer.Circuit;

public interface ICircuitUserService
{
    ConcurrentDictionary<string, CircuitUser> Circuits { get; }
    event EventHandler CircuitsChanged;
    void Connect(string circuitId, string userId);
    void Disconnect(string circuitId);
}
