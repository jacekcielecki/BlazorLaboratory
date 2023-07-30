using BlazorLaboratory.GraphQL.Dto;
using BlazorLaboratory.GraphQL.Schema.Subscriptions;
using BlazorLaboratory.GraphQL.Services;
using HotChocolate.Subscriptions;
using Mapster;

namespace BlazorLaboratory.GraphQL.Schema.Mutations;

public class Mutation
{
    private readonly CoursesRepository _coursesRepository;

    public Mutation(CoursesRepository coursesRepository)
    {
        _coursesRepository = coursesRepository;
    }

    public async Task<CourseResult> CreateCourse(CourseInputType courseInputType, [Service] ITopicEventSender topicEventSender)
    {
        CourseDto course = new CourseDto()
        {
            Id = Guid.NewGuid(),
            Name = courseInputType.Name,
            Subject = courseInputType.Subject,
            InstructorId = courseInputType.InstructorId
        };

        var result = await _coursesRepository.Create(course);
        await topicEventSender.SendAsync(nameof(Subscription.CourseCreated), course);

        return result.Adapt<CourseResult>();
    }

    public async Task<CourseResult> UpdateCourse(Guid id, CourseInputType courseInputType, [Service] ITopicEventSender topicEventSender)
    {
        //throw new GraphQLException(new Error("Course not found.", "COURSE_NOT_FOUND"));
        CourseDto course = new CourseDto()
        {
            Id = id,
            Name = courseInputType.Name,
            Subject = courseInputType.Subject,
            InstructorId = courseInputType.InstructorId
        };
        course = await _coursesRepository.Update(course);

        string updateCourseTopic = $"{course.Id}_{nameof(Subscription.CourseUpdated)}";
        await topicEventSender.SendAsync(updateCourseTopic, course);

        return course.Adapt<CourseResult>();
    }

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
            throw;
        }
    }
}

