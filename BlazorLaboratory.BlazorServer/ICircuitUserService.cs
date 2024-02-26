using System.Collections.Concurrent;
using BlazorLaboratory.BlazorServer.Data;

namespace BlazorLaboratory.BlazorServer;

public interface ICircuitUserService
{
    ConcurrentDictionary<string, CircuitUser> Circuits { get;  }
}
