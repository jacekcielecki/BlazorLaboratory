using BlazorLaboratory.BlazorUI.ApiEndpoints;
using BlazorLaboratory.BlazorUI.Dto;
using Microsoft.AspNetCore.Components;

namespace BlazorLaboratory.BlazorUI.Pages;

public partial class DapperTest
{
    [Inject] private IUserClient UserClient { get; set; } = null!;

    private List<UserDto>? _users;

    protected override async Task OnInitializedAsync()
    {
        _users = await UserClient.GetAll();
    }
}
