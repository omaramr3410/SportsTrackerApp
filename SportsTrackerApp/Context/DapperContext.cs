using Microsoft.Data.SqlClient;
using System.Data;

namespace SportsTrackerApp.Context;

public interface IDapperContext
{
    /// <summary>
    /// Method to extract a DBConnection based on the application enviroment
    /// </summary>
    /// <returns></returns>
    IDbConnection CreateConnection();
}

public class DapperContext : IDapperContext
{
    private readonly IConfiguration _configuration;
    private readonly string? _connectionString;

    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("DefaultConnection");
    }

    /// <inheritdoc/>
    public IDbConnection CreateConnection()
        => new SqlConnection(_connectionString);
}