using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Sockets;
public class Server
{
    IPHostEntry host;
    IPAddress ipAddr;
    IPEndPoint endPoint;
    Socket _server;
    Socket _client;
    public Server(string ip, int port)
    {
        host = Dns.GetHostEntry(ip);
        ipAddr = host.AddressList[0];
        endPoint = new IPEndPoint(ipAddr, port);

        _server = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        _server.Bind(endPoint);
        _server.Listen(10);
    }
    public void Start()
    {
        Thread t;
        while (true)
        {
            Console.WriteLine("Esperando Conexion");
            _client = _server.Accept();
            t = new Thread(Connection);
            t.Start(_client);
            Console.WriteLine("Se Ha Conectado un cliente");

        }


    }
    public void Connection(object s)
    {
        Socket _client = (Socket)s;
        string message;
        byte[] buffer;
        User user;
        while (true)
        {
            buffer = new byte[1024];
            _client.Receive(buffer);
            user = (User) BinarySerialization.Derializate(buffer);  
            if(user.UserName == "ad"){
               byte[] response = Encoding.ASCII.GetBytes("SUCCESS");
               _client.Send(response);
            }else{
                byte[] response = Encoding.ASCII.GetBytes("MUERETE");
               _client.Send(response);
            }
        }
    }
    private string ByteToString(byte[] buffer)
    {

        string message = Encoding.ASCII.GetString(buffer);
        int index = message.IndexOf("\0");
        if (index > 0)
        {
            message = message.Substring(0, index);
        }
        return message;
    }
}