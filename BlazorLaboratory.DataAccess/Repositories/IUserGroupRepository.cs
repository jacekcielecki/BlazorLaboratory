using BlazorLaboratory.DataAccess.Models;

namespace BlazorLaboratory.DataAccess.Repositories;
public interface IUserGroupRepository
{
    Task<IEnumerable<UserGroupModel>> GetAll();
    Task InsertAsync(UserGroupModel item);
}
