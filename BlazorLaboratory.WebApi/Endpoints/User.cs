using BlazorLaboratory.DataAccess.Data;
using BlazorLaboratory.DataAccess.Repositories;

namespace BlazorLaboratory.WebApi.Endpoints;

public static class User
{
    public static void ConfigureUserEndpoints(this WebApplication app)
    {
        app.MapGet("api/User", GetUsers);
        app.MapGet("api/User/{id}", GetUser);
        app.MapPost("api/User", InsertUser);
        app.MapPut("api/User", UpdateUser);
        app.MapDelete("api/User", DeleteUser);
    }

    private static async Task<IResult> GetUsers(IUserDataRepository data)
    {
        return Results.Ok(await data.GetAll());
    }

    private static async Task<IResult> GetUser(int id, IUserData data)
    {
        var results = await data.GetUser(id);
        return results is null ? Results.NotFound() : Results.Ok(results);
    }

    private static async Task<IResult> InsertUser(UserModel user, IUserData data)
    {
        await data.InsertUser(user);
        return Results.Ok();
    }

    private static async Task<IResult> UpdateUser(UserModel user, IUserData data)
    {
        await data.UpdateUser(user);
        return Results.Ok();
    }

    private static async Task<IResult> DeleteUser(int id, IUserData data)
    {
        await data.DeleteUser(id);
        return Results.Ok();
    }
}
