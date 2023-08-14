using AppAny.HotChocolate.FluentValidation;
using BlazorLaboratory.GraphQL.Dto;
using BlazorLaboratory.GraphQL.Middleware.UseUser;
using BlazorLaboratory.GraphQL.Schema.Mutations.Course;
using BlazorLaboratory.GraphQL.Schema.Mutations.Instructor;
using BlazorLaboratory.GraphQL.Schema.Subscriptions;
using BlazorLaboratory.GraphQL.Services;
using BlazorLaboratory.GraphQL.Validators;
using HotChocolate.Authorization;
using HotChocolate.Subscriptions;
using Mapster;

namespace BlazorLaboratory.GraphQL.Schema.Mutations;

public class Mutation
{
    private readonly CoursesRepository _coursesRepository;
    private readonly InstructorRepository _instructorRepository;

    public Mutation(CoursesRepository coursesRepository, InstructorRepository instructorRepository)
    {
        _coursesRepository = coursesRepository;
        _instructorRepository = instructorRepository;
    }

    [Authorize(Policy = "IsAdmin")]
    [UseUser]
    public async Task<CourseResult> CreateCourse([UseFluentValidation, UseValidator<CourseTypeInputValidator>] CourseInputType courseInputType,
        [Service] ITopicEventSender topicEventSender, [User] User user)
    {
        Dto.Course course = new Dto.Course()
        {
            Id = Guid.NewGuid(),
            Name = courseInputType.Name,
            Subject = courseInputType.Subject,
            InstructorId = courseInputType.InstructorId,
            CreatorId = user.Id
        };

        var result = await _coursesRepository.Create(course);
        await topicEventSender.SendAsync(nameof(Subscription.CourseCreated), course);

        return result.Adapt<CourseResult>();
    }

    [Authorize(Policy = "IsAdmin")]
    [UseUser]
    public async Task<CourseResult> UpdateCourse(Guid id, CourseInputType courseInputType, 
        [Service] ITopicEventSender topicEventSender, [User] User user)
    {
        var userId = user.Id;
        Dto.Course? currentCourseDto = await _coursesRepository.GetById(id);
        if (currentCourseDto == null)
        {
            throw new GraphQLException(new Error("Course not found.", "COURSE_NOT_FOUND"));
        }
		if (currentCourseDto?.CreatorId != userId)
        { 
            throw new GraphQLException(new Error("You do not have a permission to update this resource", "INVALID_PERMISSION"));
        }

        currentCourseDto.Name = courseInputType.Name;
        currentCourseDto.Subject = courseInputType.Subject;
        currentCourseDto.InstructorId = courseInputType.InstructorId;

        Dto.Course courseUpdated = await _coursesRepository.Update(currentCourseDto);

        string updateCourseTopic = $"{currentCourseDto.Id}_{nameof(Subscription.CourseUpdated)}";
        await topicEventSender.SendAsync(updateCourseTopic, currentCourseDto);

        return courseUpdated.Adapt<CourseResult>();
    }

    [Authorize(Policy = "IsAdmin")]
    public async Task<bool> DeleteCourse(Guid id)
    {
        //return _courses.RemoveAll(x => x.Id == id) > 1;
        try
        {
            return await _coursesRepository.Delete(id);
        }
        catch (Exception)
        {
            return false;
        }
    }

    [Authorize(Policy = "IsAdmin")]
    [UseUser]
    public async Task<InstructorResult> CreateInstructor(InstructorInputType instructorInputType)
    {
        Dto.Instructor instructor = new Dto.Instructor()
        {
            Id = Guid.NewGuid(),
            FirstName = instructorInputType.FirstName,
            LastName = instructorInputType.LastName,
            Salary = instructorInputType.Salary
        };

        var result = await _instructorRepository.Create(instructor);
        return result.Adapt<InstructorResult>();
    }
}

