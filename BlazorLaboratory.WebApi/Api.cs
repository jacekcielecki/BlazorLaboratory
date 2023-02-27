using BlazorLaboratory.DataAccess.Data;
using BlazorLaboratory.DataAccess.Repositories;

namespace BlazorLaboratory.WebApi;

public static class Api
{
    public static void ConfigureApi(this WebApplication app)
    {
        //All of endpoint mapping 
        app.MapGet("api/Users", GetUsers);
        app.MapGet("api/Users/{id}", GetUser);
        app.MapPost("api/Users", InsertUser);
        app.MapPut("api/Users", UpdateUser);
        app.MapDelete("api/Users", DeleteUser);
    }

    private static async Task<IResult> GetUsers(IUserDataRepository data)
    {
        //return Results.Ok(await data.GetUsers());
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
