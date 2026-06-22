using System.Data;
using System.Net.Sockets;

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