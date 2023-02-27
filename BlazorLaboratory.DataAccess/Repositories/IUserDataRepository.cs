using BlazorLaboratory.DataAccess.Models;

namespace BlazorLaboratory.DataAccess.Repositories;
public interface IUserDataRepository
{
    Task<IEnumerable<UserModel>> GetAll();
}
