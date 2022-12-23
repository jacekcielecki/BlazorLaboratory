using BlazorLaboratory.DataAccess.Data;

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

    private static async Task<IResult> GetUsers(IUserData data)
    {
        try
        {
            return Results.Ok(await data.GetUsers());
        }
        catch (Exception e)
        {
            return Results.Problem(e.Message);
        }
    }

    private static async Task<IResult> GetUser(int id, IUserData data)
    {
        try
        {
            var results = await data.GetUser(id);
            return results is null ? Results.NotFound() : Results.Ok(results);
        }
        catch (Exception e)
        {
            return Results.Problem(e.Message);
        }
    }

    private static async Task<IResult> InsertUser(UserModel user, IUserData data)
    {
        try
        {
            await data.InsertUser(user);
            return Results.Ok();
        }
        catch (Exception e)
        {
            return Results.Problem(e.Message);
        }
    }

    private static async Task<IResult> UpdateUser(UserModel user, IUserData data)
    {
        try
        {
            await data.UpdateUser(user);
            return Results.Ok();
        }
        catch (Exception e)
        {
            return Results.Problem(e.Message);
        }
    }

    private static async Task<IResult> DeleteUser(int id, IUserData data)
    {
        try
        {
            await data.DeleteUser(id);
            return Results.Ok();
        }
        catch (Exception e)
        {
            return Results.Problem(e.Message);
        }
    }


}
