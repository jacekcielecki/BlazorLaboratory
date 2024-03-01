using BlazorLaboratory.Shared.DTOs;

namespace BlazorLaboratory.BlazorUI.Services.Interfaces;

public interface IOpenStreetMapService
{
    public Task<CoordinatesDto?> GetCoordinates(string country, string city, string street);
}
