using System.Security.Claims;
using BlazorLaboratory.GraphQL.Dto;
using BlazorLaboratory.GraphQL.Extensions;
using HotChocolate.Resolvers;

namespace BlazorLaboratory.GraphQL.Middleware.UseUser;

public class UserMiddleware
{
    public const string USER_CONTEXT_DATA_KEY = "User";
    private readonly FieldDelegate _next;

    public UserMiddleware(FieldDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(IMiddlewareContext context)
    {
        if (context.ContextData.TryGetValue("ClaimsPrincipal", out object rawClaimsPrincipal) &&
            rawClaimsPrincipal is ClaimsPrincipal claimsPrincipal)
        {
            User user = new User()
            {
                Id = claimsPrincipal.GetUserId(),
                Email = claimsPrincipal.GetUserEmail(),
                Username = claimsPrincipal.GetUserUsername(),
                EmailVerified = bool.TryParse(claimsPrincipal.GetUserEmailVerified(), out bool result),
            };

            context.ContextData.Add(USER_CONTEXT_DATA_KEY, user);
        }

        await _next(context);
    }
}
