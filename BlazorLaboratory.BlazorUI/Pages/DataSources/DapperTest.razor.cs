using BlazorLaboratory.BlazorUI.ApiEndpoints;
using BlazorLaboratory.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BlazorLaboratory.BlazorUI.Pages.DataSources;

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
            Snackbar.Add($"{e.Message}, returning mocked data instead!", Severity.Info);
            _users = new List<UserDto>
            {
                new () { Id = new Guid(), FirstName = "John1", LastName = "Scott1", ContactDetailsId = 1, ContactDetails = new ContactDetailsDto { City = "London", Id = 1, PhoneNumber = "123456789"}},
                new () { Id = new Guid(), FirstName = "John2", LastName = "Scott2", ContactDetailsId = 2, ContactDetails = new ContactDetailsDto { City = "London", Id = 1, PhoneNumber = "123456789"}},
                new () { Id = new Guid(), FirstName = "John3", LastName = "Scott3", ContactDetailsId = 3, ContactDetails = new ContactDetailsDto { City = "London", Id = 1, PhoneNumber = "123456789"}},
            };
        }
    }

    private void DeleteItem(Guid id)
    {
        var item = _users.First(x => x.Id == id);
        _users.Remove(item);
        StateHasChanged();
    }
}
