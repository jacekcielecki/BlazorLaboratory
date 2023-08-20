using BlazorLaboratory.BlazorUI.GraphQLGenerated;
using BlazorLaboratory.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using StrawberryShake;
using Subject = BlazorLaboratory.BlazorUI.GraphQLGenerated.Subject;

namespace BlazorLaboratory.BlazorUI.Pages.GraphPage;

public partial class CreateEditCourseDialog
{
    [CascadingParameter] private MudDialogInstance MudDialog { get; set; } = null!;
    [Parameter] public Guid? ItemToUpdateId { get; set; }
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
        await GetInstructors();
        if (ItemToUpdateId != null)
        {
            await GetItemToUpdateDetails();
        }
    }

    private async Task GetInstructors()
    {
        try
        {
            IOperationResult<IGetInstructorsResult> instructorsResult = await GraphClient.GetInstructors.ExecuteAsync();
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

    private async Task GetItemToUpdateDetails()
    {
        try
        {
            IOperationResult<IGetCourseByIdResult> courseToUpdateResult = await GraphClient.GetCourseById.ExecuteAsync((Guid)ItemToUpdateId!);
            if (courseToUpdateResult.IsErrorResult())
            {
                Snackbar.Add(courseToUpdateResult.Errors.First().Message, Severity.Error);
                return;
            }

            _newCourseName = courseToUpdateResult.Data!.CourseById.Name;
            _newCourseSubject = (CourseSubject)courseToUpdateResult.Data.CourseById.Subject;
            _newCourseInstructor = _instructors.FirstOrDefault(x => x.Id == courseToUpdateResult.Data.CourseById.InstructorId)!;
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
            if (ItemToUpdateId != null)
            { 
                IOperationResult<IUpdateCourseResult> updateResult = await GraphClient.UpdateCourse.ExecuteAsync((Guid)ItemToUpdateId, new CourseInputTypeInput
                {
                   Name = _newCourseName,
                   Subject = (Subject)_newCourseSubject,
                   InstructorId = _newCourseInstructor.Id
                });
                if (updateResult.IsErrorResult())
                {
                    Snackbar.Add(updateResult.Errors.First().Message, Severity.Error);
                    return;
                }
                Snackbar.Add($"Course {updateResult.Data.UpdateCourse.Name} has been successfully updated");
            }
            else
            {
                IOperationResult<ICreateCourseResult> createResult = await GraphClient.CreateCourse.ExecuteAsync(new CourseInputTypeInput
                {
                    Name = _newCourseName,
                    Subject = (Subject)_newCourseSubject,
                    InstructorId = _newCourseInstructor.Id
                });
                if (createResult.IsErrorResult())
                {
                    Snackbar.Add(createResult.Errors.First().Message, Severity.Error);
                    return;
                }
                Snackbar.Add($"Course {createResult.Data.CreateCourse.Name} has been successfully created");
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
