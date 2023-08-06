using BlazorLaboratory.GraphQL.Schema.Mutations.Course;
using FluentValidation;

namespace BlazorLaboratory.GraphQL.Validators;

public class CourseTypeInputValidator : AbstractValidator<CourseInputType>
{
    public CourseTypeInputValidator()
    {
        RuleFor(x => x.Name).MinimumLength(4).MaximumLength(40)
            .WithMessage("Course name must be between 4 and 40 characters")
            .WithErrorCode("VALIDATION_ERROR_COURSE_TYPE_INPUT");
    }
}
