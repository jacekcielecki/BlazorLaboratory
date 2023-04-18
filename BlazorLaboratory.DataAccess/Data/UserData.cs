using BlazorLaboratory.DataAccess.Contracts;
using BlazorLaboratory.DataAccess.Models;

namespace BlazorLaboratory.DataAccess.Data;

public class UserData : IUserData
{
    private readonly ISqlDataAccess _db;

    public UserData(ISqlDataAccess db)
    {
        _db = db;
    }

    public Task<IEnumerable<UserModel>> GetUsers() =>
        _db.LoadData<UserModel, dynamic>("dbo.spUser_GetAll", new { });

    public async Task<UserModel?> GetUser(Guid id)
    {
        var results = await _db.LoadData<UserModel, dynamic>(
            "dbo.spUser_Get",
            new { Id = id });
        return results.FirstOrDefault();
    }

    public Task InsertUser(UserModel user) =>
        _db.SaveData("dbo.spUser_Insert", new { user.FirstName, user.LastName });

    public Task UpdateUser(UserModel user) =>
        _db.SaveData("dbo.spUser_Update", user);

    public Task DeleteUser(Guid id) =>
        _db.SaveData("dbo.spUser_Delete", new { Id = id });
}
