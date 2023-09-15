using BlazorLaboratory.DataAccess.Models;

namespace BlazorLaboratory.BlazorUI.Services.Interfaces;

public interface IOpenStreetMapService
{
    public Task<OSMGetCoordinatesResponse?> GetCoordinates(string country, string city, string street);
}
