using System;
using System.Data;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.Encodings.Web;
namespace live_message_app.Services;
using static System.Console;

public class packet
{
    public string Type { get; set; }

    public int From { get; set; }

    public int To { get; set; }

    public string Text { get; set; }
}
public class Network
{
    private TcpClient? _client;

    public bool Connect(string ip, int port)
    {
        try
        {
            var client = new TcpClient();
            client.Connect(ip, port);
            _client = client;
            return true;
        }
        catch
        {
            return false;
        }
    }
    public void sendpacket(packet tempo)
    {
        string json = JsonSerializer.Serialize(tempo);
        byte[] data=Encoding.UTF8.GetBytes(json);
        NetworkStream stream = _client.GetStream();
        stream.Write(data);

    }

    public packet start_recieving()
    {
        NetworkStream stream = _client.GetStream();
        byte[] buffer = new byte[4096];
        while (true)
        {
            int bytes = stream.Read(buffer, 0, buffer.Length);
            string tempo = Encoding.UTF8.GetString(buffer,0,bytes);
            WriteLine("packet broadcasted:");
            WriteLine(tempo);
            packet ? p =JsonSerializer.Deserialize<packet>(tempo);
            return p;
        }
    }
}


