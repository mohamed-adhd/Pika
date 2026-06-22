using System.Net;
using System.Net.Sockets;
using System.Text;

TcpListener server = new TcpListener(IPAddress.Any, 8000);
server.Start();
Console.WriteLine("Server started on port 8000");
List<TcpClient> clients=new();
while (true)
{
    TcpClient client = server.AcceptTcpClient();
    clients.Add(client);
    Console.WriteLine("client connected");
    Task.Run(() => handle(client));
}

void handle(TcpClient client)
{
    NetworkStream stream = client.GetStream();
    byte[] buffer = new byte[4096];
    while (true)
    {
        int bytes = stream.Read(buffer, 0, buffer.Length);
        string tempo = Encoding.UTF8.GetString(buffer,0,bytes);
        Console.WriteLine("packet received:");
        Console.WriteLine(tempo);
        foreach (TcpClient c in clients)
        {
            NetworkStream outsream = c.GetStream();
            outsream.Write(buffer, 0, bytes);
        }
    }
}