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
        app.MapDelete("api/UserGroup/{id:Guid}", DeleteUserGroup);
        app.MapPut("api/UserGroup/Add/{userId:Guid}/{groupId:Guid}", AddUserToGroup);
        app.MapPut("api/UserGroup/Remove/{userId:Guid}/{groupId:Guid}", RemoveUserFromGroup);
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

    private static async Task<IResult> DeleteUserGroup(IUserGroupRepository data, Guid id)
    {
        await data.DeleteAsync(id);
        return Results.Ok();
    }

    private static async Task<IResult> AddUserToGroup(IUserGroupRepository data, Guid userId, Guid groupId)
    {
        await data.AddItemAsync(userId, groupId);
        return Results.Ok();
    }
    
    private static async Task<IResult> RemoveUserFromGroup(IUserGroupRepository data, Guid userId, Guid groupId)
    {
        await data.RemoveItemAsync(userId, groupId);
        return Results.Ok();
    }
}
