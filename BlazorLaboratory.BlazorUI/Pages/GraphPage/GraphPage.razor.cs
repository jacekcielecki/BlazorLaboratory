using BlazorLaboratory.BlazorUI.Shared;
using MudBlazor;
using StrawberryShake;

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
        DialogOptions dialogOptions = new() { MaxWidth = MaxWidth.Medium, FullWidth = true, NoHeader = false };
        DialogParameters parameters = new()
        {
            { "ButtonText", "Confirm" },
            { "ContentText", "Are you sure you want to delete selected course? This process cannot be undone." },
        };
        var dialog = DialogService.Show<ConfirmationDialog>("Confirm Delete", parameters, dialogOptions);
        var result = await dialog.Result;
        if (!result.Cancelled)
        {
            try
            {
                var deleteItemResult = await GraphClient.DeleteCourse.ExecuteAsync(id);
                if (deleteItemResult.IsErrorResult())
                {
                    Snackbar.Add(deleteItemResult.Errors.First().Message, Severity.Info);
                }
                else
                {
                    Snackbar.Add("Item deleted!", Severity.Success);
                }
            }
            catch (Exception e)
            {
                Snackbar.Add(e.Message, Severity.Info);
            }
        }
    }

    private async Task CreateItem()
    {
        DialogOptions dialogOptions = new() { MaxWidth = MaxWidth.Medium, FullWidth = true, NoHeader = false };
        var dialog = DialogService.Show<CreateEditCourseDialog>("Create Course", dialogOptions);
        var result = await dialog.Result;
        if (!result.Cancelled)
        {
            Snackbar.Add("Item created");
        }
    }
}
