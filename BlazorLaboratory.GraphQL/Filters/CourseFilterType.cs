using BlazorLaboratory.GraphQL.Schema.Queries.Course;
using HotChocolate.Data.Filters;

namespace BlazorLaboratory.GraphQL.Filter;

public class CourseFilterType : FilterInputType<CourseType>
{
    protected override void Configure(IFilterInputTypeDescriptor<CourseType> descriptor)
    {
        //disable filtering by Students property
        descriptor.Ignore(x => x.Students);
    }
}
