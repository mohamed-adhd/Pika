using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Intrinsics.X86;
using Microsoft.Data.Sqlite;
namespace live_message_app.Services;
using static Console;

public struct Message
{
    public string Text;
    public int from_id;
    public int to_id;
    public int order;

    
}
public class database
{
    private string path = "Data Source=/home/bro/my-creations/live-message-app/databases/admin.db ";
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

    public List<Message> add(string username, string name, string password, string gmail)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password) ||
            string.IsNullOrEmpty(gmail))
        {
            return false;
        }

        using var con = new SqliteConnection(path);
        con.Open();
        var cmd = con.CreateCommand();
        cmd.CommandText = "INSERT INTO users(username,name,password,gmail) VALUES ($user,$name,$passwd,$gmail);";
        cmd.Parameters.AddWithValue("$user", username);
        cmd.Parameters.AddWithValue("$name", name);
        cmd.Parameters.AddWithValue("$passwd", password);
        cmd.Parameters.AddWithValue("$gmail", gmail);

        long res = (long)cmd.ExecuteNonQuery()!;
        return res > 0;
        
    }

    public List<Message> Fetchmessages(int id)
    {
        List<Message> tempo=new();
        using var con = new SqliteConnection(path);
        con.Open();
        var cmd = con.CreateCommand();
        cmd.CommandText = "SELECT * FROM chats WHERE from_di=$f OR to_id=$t;";
        cmd.Parameters.AddWithValue("$f", id);  
        cmd.Parameters.AddWithValue("$t", id);  
        using var ls =cmd.ExecuteReader()!;
        while (ls.Read())
        {
            Message temp;
            temp.from_id = ls.GetInt32(0);
            temp.to_id = ls.GetInt32(1);
            temp.Text = ls.GetString(2);
            temp.order = ls.GetInt32(3);
            tempo.Add(temp);
        }
    }
    
}