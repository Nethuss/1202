using Npgsql;

namespace Success.Data;

public static class Db
{
    private static readonly string _connection = "Host=localhost;Port=5432;Database=elzhur;Username=postgres;Password=2121";

    public static NpgsqlConnection GetConnection()
    {
        var conn = new NpgsqlConnection(_connection);
        conn.Open();
        return conn;
    }
}