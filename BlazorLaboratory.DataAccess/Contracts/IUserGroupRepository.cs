using BlazorLaboratory.DataAccess.Models;

namespace BlazorLaboratory.DataAccess.Contracts;
public interface IUserGroupRepository
{
    Task<IEnumerable<UserGroupModel>> GetAllAsync();
    Task InsertAsync(UserGroupModel item);
    Task UpdateAsync(UserGroupModel item);
    Task DeleteAsync(UserGroupModel item);
    Task<UserGroupModel> GetByIdAsync(Guid id);
    Task AddItemAsync(Guid userId);
    Task RemoveItemAsync(Guid userId);
}
