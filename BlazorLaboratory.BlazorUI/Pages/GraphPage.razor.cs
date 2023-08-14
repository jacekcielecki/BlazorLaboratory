using MudBlazor;

namespace BlazorLaboratory.BlazorUI.Pages;

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
}
