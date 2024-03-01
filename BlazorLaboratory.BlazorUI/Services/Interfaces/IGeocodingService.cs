using BlazorLaboratory.Shared.DTOs;

namespace BlazorLaboratory.BlazorUI.Services.Interfaces;

public interface IGeocodingService
{
    Task<GetCoordinatesResponseDto?> GetCoordinates(AddressDto address);
}
