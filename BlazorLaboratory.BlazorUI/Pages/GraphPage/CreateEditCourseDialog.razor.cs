using BlazorLaboratory.BlazorUI.GraphGL;
using BlazorLaboratory.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using StrawberryShake;

namespace BlazorLaboratory.BlazorUI.Pages.GraphPage;

public partial class CreateEditCourseDialog
{
    [CascadingParameter] private MudDialogInstance MudDialog { get; set; } = null!;
    [Inject] private IGraphClient GraphClient { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;

    private bool _showLoadIndicator = false;
    private List<InstructorDto> _instructors = new List<InstructorDto>();
    private string _newCourseName = "";
    private CourseSubject _newCourseSubject = CourseSubject.Mathematics;
    private InstructorDto _newCourseInstructor = new InstructorDto();
    private enum CourseSubject { Mathematics, Science, History }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var instructorsResult = await GraphClient.GetInstructors.ExecuteAsync();
            if (instructorsResult.IsErrorResult())
            {
                Snackbar.Add(instructorsResult.Errors.First().Message, Severity.Error);
                return;
            }
            _instructors = instructorsResult.Data.Instructors.Select(i => new InstructorDto
            {
                Id = i.Id,
                FirstName = i.FirstName,
                LastName = i.LastName,
                Salary = 0
            }).ToList();

            _newCourseInstructor = _instructors.First();
        }
        catch (Exception e)
        {
            Snackbar.Add(e.Message, Severity.Error);
        }
    }

    private async Task Save()
    {
        try
        {
            var result = await GraphClient.CreateCourse.ExecuteAsync(new CourseInputTypeInput
            {
                Name = _newCourseName,
                Subject = Subject.Science,
                InstructorId = _newCourseInstructor.Id
            });
            if (result.IsErrorResult())
            {
                Snackbar.Add(result.Errors.First().Message, Severity.Error);
            }
        }
        catch (Exception e)
        {
            Snackbar.Add(e.Message, Severity.Error);
        }
    }

    private Func<InstructorDto, string> _instructorConvertFunc = i => $"{i?.FirstName} {i?.LastName}";

    async Task Submit()
    {
        await Save();
        MudDialog.Close(DialogResult.Ok(true));
    }

    void Cancel() => MudDialog.Cancel();
}
