using BlazorLaboratory.BlazorUI.GraphGL;
using BlazorLaboratory.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BlazorLaboratory.BlazorUI.Pages.GraphPage;

public partial class EditCourseDialog
{
    [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
    [Parameter] public Guid CourseId { get; set; }
    //[Inject] private IGraphClient GraphClient { get; set; } = null!;

    private CourseDto _course;
    private bool _showLoadIndicator;

    protected override async Task OnInitializedAsync()
    {
        _course = new CourseDto();
        //var course = await GraphClient.GetCourseById.ExecuteAsync();
    }

    private async Task OnValidSubmit()
    {

    }
}
