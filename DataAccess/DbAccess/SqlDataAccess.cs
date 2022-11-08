using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace BlazorLaboratory.DataAccess.DbAccess;

public class SqlDataAccess
{
    private readonly IConfiguration _config;

    public SqlDataAccess(IConfiguration config)
    {
        _config = config;
    }
}
