using System;
using System.IO;
using Microsoft.Data.Sqlite;
namespace live_message_app.Services;
using static Console;
public class database
{
    private string path = "Data Source=/home/bro/my-creations/live-message-app/databases/data.db ";

    public bool check_login(string username, string passwd)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(passwd))
            return false;
        using var con = new SqliteConnection(path);
        con.Open();
        var cmd = con.CreateCommand();
        cmd.CommandText="SELECT COUNT(*) FROM users WHERE username=$usr AND password=$psd;";
        cmd.Parameters.AddWithValue("$usr", username);
        cmd.Parameters.AddWithValue("$psd", passwd);

        long res = (long)cmd.ExecuteScalar()!;
        Console.WriteLine($"DEBUG: user='{username}' pass='{passwd}' count={res}");
        return res > 0;
    }
    
}