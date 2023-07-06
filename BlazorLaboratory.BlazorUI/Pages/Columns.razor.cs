using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BlazorLaboratory.BlazorUI.Pages;


public partial class Columns
{
    private string _content;

    protected override void OnInitialized()
    {
        string partnerHubUrl = "https://www.google.pl";
        string html1 = $"You cannot unlock the project in Building Planner because it has been locked in the <a href=\"{partnerHubUrl}\" class=\"font-weight-bold link-dark\" target=\"_blank\">Partner Hub</a>. Go to the <a href=\"{partnerHubUrl}\" class=\"font-weight-bold link-dark\" target=\"_blank\">Partner Hub</a> to unlock the project.";
       
        _content = string.Format(html1, partnerHubUrl);

        _snackbar.Add(_content, Severity.Error);
    }
}
