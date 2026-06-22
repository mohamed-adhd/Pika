using System.Data;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.Encodings.Web;
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
    }
}
public void sendpacket(packet tempo,TcpClient _client)
{
    string json = JsonSerializer.Serialize(tempo);
    byte[] data=Encoding.UTF8.GetBytes(json);
    NetworkStream stream = _client.GetStream();
    stream.Write(data);

}

