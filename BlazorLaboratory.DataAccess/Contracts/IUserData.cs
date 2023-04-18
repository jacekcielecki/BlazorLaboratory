using BlazorLaboratory.DataAccess.Models;

namespace BlazorLaboratory.DataAccess.Contracts;

public interface IUserData
{
    Task<IEnumerable<UserModel>> GetUsers();
    Task<UserModel?> GetUser(Guid id);
    Task InsertUser(UserModel user);
    Task UpdateUser(UserModel user);
    Task DeleteUser(Guid id);
}