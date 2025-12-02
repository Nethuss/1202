using Success.Models;
using Success.Data;
using Npgsql;
using System.Collections.Generic;

namespace Success.Services;

public class UserService
{
    public List<User> GetAllUsers()
    {
        var users = new List<User>();

        using var conn = Db.GetConnection();
        using var cmd = new NpgsqlCommand("SELECT id, username FROM public.users order by id desc", conn);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            users.Add(new User
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1)
            });
        }

        return users;
    }

    public void CreateUser(string name)
    {
        using var conn = Db.GetConnection();
        using var cmd = new NpgsqlCommand(
            "INSERT INTO public.users (username) VALUES (@name)", conn);

        cmd.Parameters.AddWithValue("@name", name);
        cmd.ExecuteNonQuery();
    }
}
