using BlazorLaboratory.DataAccess.Models;

namespace BlazorLaboratory.DataAccess.Contracts;
public interface IUserDataRepository
{
    Task<IEnumerable<UserModel>> GetAll();
    Task<int> InsertMany(List<UserModel> users);
}
