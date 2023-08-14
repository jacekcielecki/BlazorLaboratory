using BlazorLaboratory.Shared.DTOs;
using Refit;

namespace BlazorLaboratory.BlazorUI.ApiEndpoints;

public interface IUserClient
{
    [Get("/api/User")]
    Task<List<UserDto>> GetAll();
}
