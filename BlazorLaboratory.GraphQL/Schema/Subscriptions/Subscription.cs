using BlazorLaboratory.GraphQL.Schema.Mutations.Course;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;

namespace BlazorLaboratory.GraphQL.Schema.Subscriptions;

public class Subscription
{
    [Subscribe]
    public CourseResult CourseCreated([EventMessage] CourseResult course) => course;

    [SubscribeAndResolve]
    public async ValueTask<ISourceStream<CourseResult>> CourseUpdated(Guid courseId, [Service] ITopicEventReceiver topicEventReceiver)
    {
        string topicName = $"{courseId}_{nameof(Subscription.CourseUpdated)}";
        return await topicEventReceiver.SubscribeAsync<CourseResult>(topicName);
    }
}
