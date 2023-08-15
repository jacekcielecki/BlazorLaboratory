using BlazorLaboratory.BlazorUI.Shared;
using MudBlazor;

namespace BlazorLaboratory.BlazorUI.Pages.GraphPage;

public partial class GraphPage
{
    protected override async Task OnInitializedAsync()
    {
        try
        {
            var valueFromGraph = await GraphClient.GetInstructions.ExecuteAsync();
        }
        catch (Exception e)
        {
            Snackbar.Add(e.Message, Severity.Info);
        }
    }

    private async Task DeleteItem(Guid id)
    {
        DialogOptions dialogOptions = new() { MaxWidth = MaxWidth.ExtraLarge, NoHeader = false };
        DialogParameters parameters = new()
        {
            { "ButtonText", "Confirm" },
            { "ContentText", "Are you sure you want to delete selected course? This process cannot be undone." }
        };
        var dialog = DialogService.Show<ConfirmationDialog>("Confirm Delete", parameters, dialogOptions);
        var result = await dialog.Result;
        if (!result.Cancelled)
        {
            Snackbar.Add("Item deleted");
        }
    }

    private async Task EditItem(Guid id)
    {
        DialogOptions dialogOptions = new() { MaxWidth = MaxWidth.ExtraLarge, NoHeader = false };
        var dialog = DialogService.Show<EditCourseDialog>("Edit Course", dialogOptions);
        var result = await dialog.Result;
        if (!result.Cancelled)
        {
            Snackbar.Add("Item updated");
        }
    }
}
