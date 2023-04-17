using BlazorLaboratory.DataAccess.Repositories;

namespace BlazorLaboratory.WebApi.Endpoints;

public static class UserGroup
{
    public static void ConfigureUserGroupEndpoints(this WebApplication app)
    {
        app.MapGet("api/UserGroup", GetUserGroups);
        app.MapPost("api/UserGroup", InsertUserGroup);
    }

    private static async Task<IResult> GetUserGroups(IUserGroupRepository data)
    {
        return Results.Ok(await data.GetAll());
    }

    private static async Task<IResult> InsertUserGroup(IUserGroupRepository data, UserGroupModel item)
    {
        await data.InsertAsync(item);
        return Results.Ok();
    }
}
