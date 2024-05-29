using System;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace Cliente;
public class Client
{
    IPHostEntry host;
    IPAddress ipAddr;
    IPEndPoint endPoint;
    Socket _client;
    public Client(string ip, int port)
    {
        host = Dns.GetHostEntry(ip);
        ipAddr = host.AddressList[0];
        endPoint = new IPEndPoint(ipAddr, port);

        _client = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        
    }
    public void Start()
    {
        _client.Connect(endPoint);
    }
    public void Send(string mensaje){
        byte[] buffer = Encoding.ASCII.GetBytes(mensaje);
        _client.Send(buffer);
    }
     public void SendObject(object mensaje){
        _client.Send(BinarySerialization.Serializate(mensaje));
    }
    public string Receive(){
        byte[] buffer = new byte[1024];
        _client.Receive(buffer);
        return ByteToString(buffer);
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