using BlazorLaboratory.GraphQL.Schema.Queries.Course;
using HotChocolate.Data.Sorting;

namespace BlazorLaboratory.GraphQL.Schema.Sorters;

public class CourseSortType : SortInputType<CourseType>
{
    protected override void Configure(ISortInputTypeDescriptor<CourseType> descriptor)
    {
        descriptor.Ignore(x => x.Id);
        descriptor.Ignore(x => x.InstructorId);
    }
}
