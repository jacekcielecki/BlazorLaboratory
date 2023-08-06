using BlazorLaboratory.GraphQL.Schema.Queries.User;
using FirebaseAdmin;
using FirebaseAdmin.Auth;

namespace BlazorLaboratory.GraphQL.DataLoaders;

public class UserDataLoader : BatchDataLoader<string, UserType>
{
    private readonly FirebaseAuth _firebaseAuth;
    private const int MAX_FIREBASE_BATCH_SIZE = 100;

    public UserDataLoader(FirebaseApp firebaseApp, IBatchScheduler batchScheduler) 
        : base(batchScheduler, new DataLoaderOptions()
        {
            MaxBatchSize = MAX_FIREBASE_BATCH_SIZE
        })
    {
        _firebaseAuth  = FirebaseAuth.GetAuth(firebaseApp);
    }

    protected override async Task<IReadOnlyDictionary<string, UserType>> LoadBatchAsync(IReadOnlyList<string> userIds, CancellationToken cancellationToken)
    {
        List<UidIdentifier> userIdentifiers = userIds.Select(x => new UidIdentifier(x)).ToList();
        GetUsersResult usersResult = await _firebaseAuth.GetUsersAsync(userIdentifiers, cancellationToken);

        return usersResult.Users.Select(u => new UserType()
        {
            Id = u.Uid,
            Username = u.Email
        }).ToDictionary(u => u.Id);
    }
}
