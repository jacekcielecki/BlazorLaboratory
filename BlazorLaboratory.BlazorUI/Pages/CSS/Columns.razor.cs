using MudBlazor;

namespace BlazorLaboratory.BlazorUI.Pages.CSS;

public partial class Columns
{
    private string? _content;

    protected override void OnInitialized()
    {
        string googleUrl = "https://www.google.pl";
        string message = $"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore " +
                         $"<a href=\"{googleUrl}\" class=\"font-weight-bold link-dark\" target=\"_blank\">et dolore magna aliqua.</a>. " +
                         $"Ut enim ad minim veniam <a href=\"{googleUrl}\" class=\"font-weight-bold link-dark\" target=\"_blank\">Google</a> , quis nostrud exercitation ullamco laboris nisi.";
        _content = string.Format(message, googleUrl);
        Snackbar.Add(_content, Severity.Error);
    }
}