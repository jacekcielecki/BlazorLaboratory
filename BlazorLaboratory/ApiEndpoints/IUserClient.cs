using BlazorLaboratory.BlazorUI.Dto;
using Refit;

namespace BlazorLaboratory.BlazorUI.ApiEndpoints;

public interface IUserClient
{
    [Get("/api/Users")]
    Task<List<UserDto>> GetAll();
}
