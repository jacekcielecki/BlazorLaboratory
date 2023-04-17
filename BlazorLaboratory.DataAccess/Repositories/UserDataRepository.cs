using BlazorLaboratory.DataAccess.Models;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using BlazorLaboratory.DataAccess.Contracts;

namespace BlazorLaboratory.DataAccess.Repositories;
public class UserDataRepository : IUserDataRepository
{
    private readonly IConfiguration _config;

    public UserDataRepository(IConfiguration config)
    {
        _config = config;
    }

    public async Task<IEnumerable<UserModel>> GetAll()
    {
        using IDbConnection db = new SqlConnection(_config.GetConnectionString("default"));
        const string sql = @"SELECT us.*, cd.* 
                           FROM [dbo].[User] us 
                           LEFT JOIN [dbo].[ContactDetails] cd on us.[ContactDetailsId] = cd.[Id]";

        return await db.QueryAsync<UserModel, ContactDetailsModel, UserModel>(sql,
            (user, contact) => { user.ContactDetails = contact; return user; }, commandType: CommandType.Text);
    }
}
