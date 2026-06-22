using System.Net;
using System.Net.Sockets;

TcpListener server = new TcpListener(IPAddress.Any, 8000);
server.Start();
Console.WriteLine("Server started on port 8000");
List<TcpClient> clients=new();
while (true)
{
    TcpClient client = server.AcceptTcpClient();
    clients.Add(client);
    Console.WriteLine("client connected");
}

void handle(TcpClient client)
{
    
}