using BlazorLaboratory.BlazorUI.ApiEndpoints;
using BlazorLaboratory.BlazorUI.Dto;
using BlazorLaboratory.BlazorUI.Enum;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BlazorLaboratory.BlazorUI.Pages;

public partial class DapperTest
{
    [Inject] private IUserClient UserClient { get; set; }
    [Inject] private ISnackbar Snackbar { get; set; }

    private List<UserDto>? _users;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            _users = await UserClient.GetAll();
        }
        catch (Exception e)
        {
            Snackbar.Add(e.Message, Severity.Info);
            Snackbar.Add("Unable to connect to api, returning mocked data instead!", Severity.Info);
            _users = new List<UserDto>()
            {
                new () { Id = 1, FirstName = "User1FirstName", LastName = "User1LastName", Role = UserRole.Administrator },
                new () { Id = 2, FirstName = "User2FirstName", LastName = "User2LastName", Role = UserRole.CardsCreator },
                new () { Id = 3, FirstName = "User3FirstName", LastName = "User3LastName", Role = UserRole.Viewer },
                new () { Id = 4, FirstName = "User4FirstName", LastName = "User4LastName", Role = UserRole.Administrator },
                new () { Id = 5, FirstName = "User5FirstName", LastName = "User5LastName", Role = UserRole.CardsCreator },
                new () { Id = 6, FirstName = "User6FirstName", LastName = "User6LastName", Role = UserRole.Viewer }
            };
        }
    }
}
