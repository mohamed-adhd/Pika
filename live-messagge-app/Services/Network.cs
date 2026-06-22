using System.Data;
using System.Net.Sockets;
namespace live_message_app.Services;


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
        _client = new TcpClient();
        try
        {
            _client.Connect(ip, port);
            return true;
        }
        catch
        {
            return false;
        }

        return true;
    }
}

