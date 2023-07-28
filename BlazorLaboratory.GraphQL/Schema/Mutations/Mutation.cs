using BlazorLaboratory.GraphQL.Enums;

namespace BlazorLaboratory.GraphQL.Schema.Mutations;

public class Mutation
{
    private readonly List<CourseResult> _courses;

    public Mutation()
    {
        _courses = new List<CourseResult>();
    }

    public CourseResult CreateCourse(CourseInputType courseInputType)
    {
        CourseResult courseType = new CourseResult()
        {
            Id = Guid.NewGuid(),
            Name = courseInputType.Name,
            Subject = courseInputType.Subject,
            InstructorId = Guid.NewGuid()
        };

        _courses.Add(courseType);
        return courseType;
    }

    public CourseResult UpdateCourse(Guid id, CourseInputType courseInputType)
    {
        CourseResult course = _courses.FirstOrDefault(x => x.Id == id);
        if (course is null)
        {
            throw new GraphQLException(new Error("Course not found.", "COURSE_NOT_FOUND"));
        }

        CourseResult courseType = new CourseResult()
        {
            Id = Guid.NewGuid(),
            Name = courseInputType.Name,
            Subject = courseInputType.Subject,
            InstructorId = Guid.NewGuid()
        };

        _courses.Add(courseType);
        return courseType;
    }

    public bool DeleteCourse(Guid id)
    {
        return _courses.RemoveAll(x => x.Id == id) > 1;
    }
}
