using Infrastructure.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
