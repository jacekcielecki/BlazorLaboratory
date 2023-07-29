using BlazorLaboratory.GraphQL.Enums;
using BlazorLaboratory.GraphQL.Schema.Subscriptions;
using HotChocolate.Subscriptions;

namespace BlazorLaboratory.GraphQL.Schema.Mutations;

public class Mutation
{
    private readonly List<CourseResult> _courses;

    public Mutation()
    {
        _courses = new List<CourseResult>();
    }

    public async Task<CourseResult> CreateCourse(CourseInputType courseInputType, [Service] ITopicEventSender topicEventSender)
    {
        CourseResult course = new CourseResult()
        {
            Id = Guid.NewGuid(),
            Name = courseInputType.Name,
            Subject = courseInputType.Subject,
            InstructorId = Guid.NewGuid()
        };

        _courses.Add(course);
        await topicEventSender.SendAsync(nameof(Subscription.CourseCreated), course);

        return course;
    }

    public async Task<CourseResult> UpdateCourse(Guid id, CourseInputType courseInputType, [Service] ITopicEventSender topicEventSender)
    {
        CourseResult course = _courses.FirstOrDefault(x => x.Id == id);
        if (course is null)
        {
            throw new GraphQLException(new Error("Course not found.", "COURSE_NOT_FOUND"));
        }

        course.Name = courseInputType.Name;
        course.Subject = courseInputType.Subject;
        course.InstructorId = Guid.NewGuid();

        string updateCourseTopic = $"{course.Id}_{nameof(Subscription.CourseUpdated)}";
        await topicEventSender.SendAsync(updateCourseTopic, course);

        return course;
    }

    public bool DeleteCourse(Guid id)
    {
        return _courses.RemoveAll(x => x.Id == id) > 1;
    }
}
