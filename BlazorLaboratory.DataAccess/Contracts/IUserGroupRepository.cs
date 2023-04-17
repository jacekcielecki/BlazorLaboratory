using BlazorLaboratory.DataAccess.Models;

namespace BlazorLaboratory.DataAccess.Contracts;
public interface IUserGroupRepository
{
    Task<IEnumerable<UserGroupModel>> GetAll();
    Task InsertAsync(UserGroupModel item);
}
