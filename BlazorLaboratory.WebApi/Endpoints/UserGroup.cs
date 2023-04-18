using BlazorLaboratory.DataAccess.Contracts;

namespace BlazorLaboratory.WebApi.Endpoints;

public static class UserGroup
{
    public static void ConfigureUserGroupEndpoints(this WebApplication app)
    {
        app.MapGet("api/UserGroup", GetAllUserGroup);
        app.MapGet("api/UserGroup/{id:Guid}", GetUserGroupById);
        app.MapPost("api/UserGroup", InsertUserGroup);
        app.MapPut("api/UserGroup", UpdateUserGroup);
    }

    private static async Task<IResult> GetAllUserGroup(IUserGroupRepository data)
    {
        return Results.Ok(await data.GetAllAsync());
    }

    private static async Task<IResult> GetUserGroupById(IUserGroupRepository data, Guid id)
    {
        var result = await data.GetByIdAsync(id);
        return result is null ? Results.NotFound() : Results.Ok(result);
    }

    private static async Task<IResult> InsertUserGroup(IUserGroupRepository data, UserGroupModel item)
    {
        await data.InsertAsync(item);
        return Results.Ok();
    }

    private static async Task<IResult> UpdateUserGroup(IUserGroupRepository data, UserGroupModel item)
    {
        await data.UpdateAsync(item);
        return Results.Ok();
    }
}
