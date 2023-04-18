using BlazorLaboratory.DataAccess.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using BlazorLaboratory.DataAccess.Contracts;

namespace BlazorLaboratory.DataAccess.Repositories;
public class UserGroupRepository : IUserGroupRepository
{
    private readonly IConfiguration _config;

    public UserGroupRepository(IConfiguration config)
    {
        _config = config;
    }

    public async Task<IEnumerable<UserGroupModel>> GetAllAsync()
    {
        using IDbConnection db = new SqlConnection(_config.GetConnectionString("default"));

        const string sql = @"SELECT ug.*, cr.*, md.*, u.*
                                    FROM [UserGroup] ug
                                    LEFT JOIN [dbo].[User] cr on ug.[CreatorId] = cr.[Id]
                                    LEFT JOIN [dbo].[User] md on ug.[ModeratorId] = md.[Id]
                                    LEFT JOIN [UserGroupUser] ugu ON ugu.UserGroupId = ug.Id
                                    LEFT JOIN [User] u ON u.Id = ugu.UserId";

        var groups = await db.QueryAsync<UserGroupModel, UserModel, UserModel, UserModel, UserGroupModel>(sql, (groups, creator, moderator, user) =>
        {
            groups.Creator = creator;
            groups.Moderator = moderator;
            if (user is null) return groups;
            groups.Users ??= new List<UserModel>();
            groups.Users.Add(user);
            return groups;
        }, splitOn: "Id", commandType: CommandType.Text);

        var result = groups.GroupBy(x => x.Id).Select(y =>
        {
            var grouped = y.First();
            if (grouped.Users is not null && grouped.Users.Count != 0)
            {
                grouped.Users = y.Select(x => x.Users.Single()).ToList();
            }
            return grouped;
        });
        return result;
    }

    public async Task<UserGroupModel> GetByIdAsync(Guid id)
    {
        using IDbConnection db = new SqlConnection(_config.GetConnectionString("default"));

        const string sql = @"SELECT ug.*, cr.*, md.*, u.*
                             FROM [UserGroup] ug
                             LEFT JOIN [dbo].[User] cr on ug.[CreatorId] = cr.[Id]
                             LEFT JOIN [dbo].[User] md on ug.[ModeratorId] = md.[Id]
                             LEFT JOIN [UserGroupUser] ugu ON ugu.UserGroupId = ug.Id
                             LEFT JOIN [User] u ON u.Id = ugu.UserId
                             WHERE ug.[Id] = @Id";

        var result = await db.QueryAsync<UserGroupModel, UserModel, UserModel, UserModel, UserGroupModel>(sql,  (groups, creator, moderator, user) =>
        {
            groups.Creator = creator;
            groups.Moderator = moderator;
            if (user is null) return groups;
            groups.Users ??= new List<UserModel>();
            groups.Users.Add(user);
            return groups;
        }, new { Id = id }, splitOn: "Id", commandType: CommandType.Text);

       return result.FirstOrDefault()!;
    }

    public async Task InsertAsync(UserGroupModel item)
    {
        using IDbConnection db = new SqlConnection(_config.GetConnectionString("default"));
        const string sql = @"INSERT INTO [dbo].[UserGroup]
                               ([Name], [Description], [CreatorId], [ModeratorId])
                               VALUES (@Name, @Description, @CreatorId, @ModeratorId)";

        await db.ExecuteAsync(sql, new
        {
            item.Name,
            item.Description,
            item.CreatorId,
            item.ModeratorId
        }, commandType: CommandType.Text);
    }

    public async Task UpdateAsync(UserGroupModel item)
    {
        using IDbConnection db = new SqlConnection(_config.GetConnectionString("default"));
        const string sql = @"UPDATE [dbo].[UserGroup] 
                                SET [Name] = @Name 
                                   ,[Description] = @Description
                                   ,[CreatorId] = @CreatorId
                                   ,[ModeratorId] = @ModeratorId
                                WHERE [Id] = @Id";

        await db.ExecuteAsync(sql, new
        {
            item.Name,
            item.Description,
            item.CreatorId,
            item.ModeratorId,
            item.Id
        }, commandType: CommandType.Text);
    }

    public async Task DeleteAsync(Guid id)
    {
        using IDbConnection db = new SqlConnection(_config.GetConnectionString("default"));
        const string sql = @"DELETE FROM [dbo].[UserGroup] WHERE [Id] = @Id";

        await db.ExecuteAsync(sql, new { Id = id }, commandType: CommandType.Text);
    }

    public async Task AddItemAsync(Guid userId, Guid groupId)
    {
        using IDbConnection db = new SqlConnection(_config.GetConnectionString("default"));
        const string sql = @"INSERT INTO [dbo].[UserGroupUser] 
                                ([UserGroupId], [UserId])
                                VALUES (@GroupId, @UserId)";

        await db.ExecuteAsync(sql, new { UserId = userId, GroupId = groupId }, commandType: CommandType.Text);
    }

    public async Task RemoveItemAsync(Guid userId, Guid groupId)
    {
        using IDbConnection db = new SqlConnection(_config.GetConnectionString("default"));
        const string sql = @"DELETE FROM [dbo].[UserGroupUser] 
                                WHERE [UserGroupId] = @GroupId
                                AND [UserId] = @UserId";

        await db.ExecuteAsync(sql, new { UserId = userId, GroupId = groupId }, commandType: CommandType.Text);
    }
}
