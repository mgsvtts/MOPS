using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SQLite;

namespace Infrastructure;

public class DbContext
{
    private readonly IConfiguration _config;
    private readonly string _connectionString;

    public DbContext(IConfiguration configuration)
    {
        _config = configuration;
        _connectionString = _config.GetConnectionString("DefaultConnection");
    }

    public IDbConnection CreateConnection()
    {
        return new SQLiteConnection(_connectionString);
    }
}
