using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Intrinsics.X86;
using Microsoft.Data.Sqlite;
namespace live_message_app.Services;
using static Console;

public struct Messagestruct
{
    public string Text{ get; set; }
    public int from_id{ get; set; }
    public int to_id{ get; set; }
    public int order{ get; set; }

    
}

public class invites
{
    public string username{ get; set; }
    public int from_id{ get; set; }
    public int to_id{ get; set; }
}
public class user
{
    public int id{ get; set; }
    public string name{ get; set; }
    public string username{ get; set; }
    public string LastMessage { get; set; }
}
public class database
{
    private string path = "Data Source=/home/bro/my-creations/live-message-app/live-messagge-app/databases/admin.db";
    public int check_login(string username, string passwd)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(passwd))
            return -1;
        using var con = new SqliteConnection(path);
        con.Open();
        var cmd = con.CreateCommand();
        cmd.CommandText="SELECT id FROM users WHERE username=$usr AND password=$psd;";
        cmd.Parameters.AddWithValue("$usr", username);
        cmd.Parameters.AddWithValue("$psd", passwd);

        using var res = cmd.ExecuteReader()!;
        Console.WriteLine($"DEBUG: user='{username}' pass='{passwd}' count={res}");
        if (res.Read()) return res.GetInt32(0);
        return -1;
    }

    public bool add(string username, string name, string password, string gmail)
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

    public string get_username(int id)
    {
        using var con = new SqliteConnection(path);
        con.Open();
        var cmd = con.CreateCommand();
        cmd.CommandText="SELECT username FROM users WHERE id=$d;";
        cmd.Parameters.AddWithValue("$d", id);
        using var res = cmd.ExecuteReader()!;
        //!Console.WriteLine($"DEBUG: user='{username}' pass='{passwd}' count={res}");-->
        user temp=new();

        res.Read();
        return res.GetString(0);


    }
    public int get_id(string s)
    {
        using var con = new SqliteConnection(path);
        con.Open();
        var cmd = con.CreateCommand();
        cmd.CommandText="SELECT id FROM users WHERE username=$d;";
        cmd.Parameters.AddWithValue("$d", s);
        using var res = cmd.ExecuteReader()!;
        //!Console.WriteLine($"DEBUG: user='{username}' pass='{passwd}' count={res}");-->
        user temp=new();

        res.Read();
        return res.GetInt32(0);


    }
    public string get_gmail(string s)
    {
        using var con = new SqliteConnection(path);
        con.Open();
        var cmd = con.CreateCommand();
        cmd.CommandText="SELECT gmail FROM users WHERE username=$d;";
        cmd.Parameters.AddWithValue("$d", s);
        using var res = cmd.ExecuteReader()!;
        //!Console.WriteLine($"DEBUG: user='{username}' pass='{passwd}' count={res}");-->
        user temp=new();

        res.Read();
        return res.GetString(0);


    }
    public string get_pass(string s)
    {
        using var con = new SqliteConnection(path);
        con.Open();
        var cmd = con.CreateCommand();
        cmd.CommandText="SELECT password FROM users WHERE username=$d;";
        cmd.Parameters.AddWithValue("$d", s);
        using var res = cmd.ExecuteReader()!;
        //!Console.WriteLine($"DEBUG: user='{username}' pass='{passwd}' count={res}");-->
        user temp=new();

        res.Read();
        return res.GetString(0);


    }
    public user search_by_id(int id,int id2)
    {
        using var con = new SqliteConnection(path);
        con.Open();
        var cmd = con.CreateCommand();
        cmd.CommandText="SELECT * FROM users WHERE id=$d;";
        cmd.Parameters.AddWithValue("$d", id);
        using var res = cmd.ExecuteReader()!;
        //!Console.WriteLine($"DEBUG: user='{username}' pass='{passwd}' count={res}");-->
        user temp=new();
        
        while (res.Read())
        {
            temp.id = id;
            temp.name=res.GetString(1);
            temp.username=res.GetString(2);
            var cmdo = con.CreateCommand();
            cmdo.CommandText="""SELECT text FROM chats WHERE (from_id = $d AND to_id = $d2) OR (from_id = $d3 AND to_id = $d4) ORDER BY "order" DESC LIMIT 1;""";
            cmdo.Parameters.AddWithValue("$d", id);
            cmdo.Parameters.AddWithValue("$d2", id2);
            cmdo.Parameters.AddWithValue("$d3", id2);
            cmdo.Parameters.AddWithValue("$d4", id);
            using var reso = cmdo.ExecuteReader()!;
            if (reso.Read())
            {
                temp.LastMessage = reso.GetString(0);
            }
            else
            {
                temp.LastMessage = "";
            }
        }

        return temp;

    }
    public List<Messagestruct> Fetchmessages(int id)
    {
        List<Messagestruct> tempo=new();
        using var con = new SqliteConnection(path);
        con.Open();
        var cmd = con.CreateCommand();
        cmd.CommandText = "SELECT * FROM chats WHERE from_id=$f OR to_id=$t;";
        cmd.Parameters.AddWithValue("$f", id);  
        cmd.Parameters.AddWithValue("$t", id);  
        using var ls =cmd.ExecuteReader()!;
        while (ls.Read())
        {
            Messagestruct temp=new();
            temp.from_id = ls.GetInt32(0);
            temp.to_id = ls.GetInt32(1);
            temp.Text = ls.GetString(2);
            temp.order = ls.GetInt32(3);
            tempo.Add(temp);
        }

        return tempo;
    }

    public List<user> Fetchfriends(List<Messagestruct> mlist,int id)
    {
        List<user> friends = new();
        List<int> temp = new();
        foreach (Messagestruct m in mlist)
        {
            if (m.from_id == id)
            {
                if (!temp.Contains(m.to_id))
                {
                    friends.Add(search_by_id(m.to_id,id));
                    temp.Add(m.to_id);
                }
                
            }else if (m.to_id == id)
            {
                if (!temp.Contains(m.from_id))
                {
                    friends.Add(search_by_id(m.from_id,id));
                    temp.Add(m.from_id);
                }
            }
        }

        return friends;
    }

    public void delete(int thid)
    {
        using var con =new  SqliteConnection();
        con.Open();
        using var cmd1 = con.CreateCommand();
        cmd1.CommandText = "DELETE FROM messages WHERE from_id = @id OR to_id = @id";
        cmd1.Parameters.AddWithValue("@id",thid);
        cmd1.ExecuteNonQuery();
    }
    public int  neword(int id1,int id2){
        using var con = new SqliteConnection(path);
        con.Open();
        var cmd = con.CreateCommand();
        cmd.CommandText = """SELECT "order" FROM chats WHERE (from_id=$id1 and to_id=$id2) or (from_id=$id3 and to_id=$id4) ORDER BY "order" DESC LIMIT 1""";
        cmd.Parameters.AddWithValue("$id1", id1);
        cmd.Parameters.AddWithValue("$id2", id2);
        cmd.Parameters.AddWithValue("$id3", id2);
        cmd.Parameters.AddWithValue("$id4", id1);

        using var res = cmd.ExecuteReader()!;
        if (res.Read())
        {
            
            return res.GetInt32(0) + 1;
        }
        return 1;
    }

    public void addmsg(string txt, int fid, int tid,int order)
    {
        using var con = new SqliteConnection(path);
        con.Open();
        var cmd = con.CreateCommand();
        cmd.CommandText = """INSERT INTO chats(from_id,to_id,text,"order") VALUES ($user,$name,$passwd,$gmail);""";
        cmd.Parameters.AddWithValue("$user", fid);
        cmd.Parameters.AddWithValue("$name", tid);
        cmd.Parameters.AddWithValue("$passwd", txt);
        cmd.Parameters.AddWithValue("$gmail", order);

        long res = (long)cmd.ExecuteNonQuery()!;

    }
    
    public List<Messagestruct> selecmsg(int id1, int id2)
    {
        List<Messagestruct> tempo = new();
        using var con = new SqliteConnection(path);
        con.Open();
        var cmd = con.CreateCommand();
        cmd.CommandText = """SELECT from_id, to_id, text, "order" FROM chats WHERE (from_id=$id1 and to_id=$id2) or (from_id=$id3 and to_id=$id4) ORDER BY "order" ASC""";;
        cmd.Parameters.AddWithValue("$id1", id1);
        cmd.Parameters.AddWithValue("$id2", id2);
        cmd.Parameters.AddWithValue("$id3", id2);
        cmd.Parameters.AddWithValue("$id4", id1);
        
        using var res=cmd.ExecuteReader()!;
        while (res.Read())
        {
            Messagestruct temp=new();
            temp.from_id = res.GetInt32(0);
            temp.to_id = res.GetInt32(1);
            temp.Text = res.GetString(2);
            temp.order = res.GetInt32(3);
            tempo.Add(temp);

        }

        return tempo;

    }

    public user search_by_username(string name)
    {
        using var con = new SqliteConnection(path);
        con.Open();
        var cmd = con.CreateCommand();
        cmd.CommandText = "SELECT * FROM users  WHERE username=$d;";
        cmd.Parameters.AddWithValue("$d", name);
        using var res= cmd.ExecuteReader();
        if (res.Read())
        {
            user temp=new();
            temp.id = res.GetInt32(0);
            temp.name = res.GetString(1);
            temp.username = res.GetString(2);
            temp.LastMessage = "";
            return temp;

        }

        return null;

    }

    public void add_invite(int fid, int tid)
    {
        using var con = new SqliteConnection(path);
        con.Open();
        var cmd = con.CreateCommand();
        cmd.CommandText = "INSERT INTO invites (from_id,to_id) VALUES ($fi,$ti);";
        cmd.Parameters.AddWithValue("$fi",fid );
        cmd.Parameters.AddWithValue("$ti",tid );
        cmd.ExecuteNonQuery();

    }

    public List<invites> fetch_invites(int id)
    {
        List<invites> tempo= new();
        using var con = new SqliteConnection(path);
        con.Open();
        var cmd = con.CreateCommand();
        cmd.CommandText = "SELECT * FROM invites WHERE to_id=$id;";
        cmd.Parameters.AddWithValue("$id", id);
        using var res=cmd.ExecuteReader();
        while (res.Read())
        {
            invites temp=new();
            temp.from_id = res.GetInt32(0);
            temp.to_id = res.GetInt32(1);
            temp.username = get_username(temp.from_id);
            tempo.Add(temp);
            
        }

        return tempo;
    }

    public void add_friends(int id1, int id2)
    {
        var con = new SqliteConnection(path);
        con.Open();
        using var cmd = con.CreateCommand();
        cmd.CommandText = """INSERT INTO chats(from_id,to_id,text,"order") VALUES ($f,$t,"Hello Lets start chatting!",0); """;
        cmd.Parameters.AddWithValue("$f",id1);
        cmd.Parameters.AddWithValue("$t",id2);
        cmd.ExecuteNonQuery();
    }

    public void delete_invite(invites i)
    {
        
        var con = new SqliteConnection(path);
        con.Open();
        using var cmd = con.CreateCommand();
        cmd.CommandText = "DELETE FROM invites WHERE from_id=$fi AND to_id=$ti;";
        cmd.Parameters.AddWithValue("$fi",i.from_id);
        cmd.Parameters.AddWithValue("$ti",i.to_id);
        cmd.ExecuteNonQuery();
    }

    
    
}