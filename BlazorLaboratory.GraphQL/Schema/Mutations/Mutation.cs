using BlazorLaboratory.GraphQL.Dto;
using BlazorLaboratory.GraphQL.Extensions;
using BlazorLaboratory.GraphQL.Schema.Mutations.Course;
using BlazorLaboratory.GraphQL.Schema.Subscriptions;
using BlazorLaboratory.GraphQL.Services;
using HotChocolate.Authorization;
using HotChocolate.Subscriptions;
using Mapster;
using System.Security.Claims;

namespace BlazorLaboratory.GraphQL.Schema.Mutations;

public class Mutation
{
    private readonly CoursesRepository _coursesRepository;

    public Mutation(CoursesRepository coursesRepository)
    {
        _coursesRepository = coursesRepository;
    }

    [Authorize(Policy = "IsAdmin")]
    public async Task<CourseResult> CreateCourse(CourseInputType courseInputType,
        [Service] ITopicEventSender topicEventSender, ClaimsPrincipal claims)
    {
        CourseDto course = new CourseDto()
        {
            Id = Guid.NewGuid(),
            Name = courseInputType.Name,
            Subject = courseInputType.Subject,
            InstructorId = courseInputType.InstructorId,
            CreatorId = claims.GetUserId()
        };

        var result = await _coursesRepository.Create(course);
        await topicEventSender.SendAsync(nameof(Subscription.CourseCreated), course);

        return result.Adapt<CourseResult>();
    }

    [Authorize(Policy = "IsAdmin")]
    public async Task<CourseResult> UpdateCourse(Guid id, CourseInputType courseInputType, 
        [Service] ITopicEventSender topicEventSender, ClaimsPrincipal claims)
    {
        var userId = claims.GetUserId();
        CourseDto? currentCourseDto = await _coursesRepository.GetById(id);
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

        CourseDto courseUpdated = await _coursesRepository.Update(currentCourseDto);

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
}

