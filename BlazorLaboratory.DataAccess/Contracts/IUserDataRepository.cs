using BlazorLaboratory.DataAccess.Models;

namespace BlazorLaboratory.DataAccess.Contracts;
public interface IUserDataRepository
{
    Task<IEnumerable<UserModel>> GetAll();
}
