using Sockets;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
Server server = new Server("localhost",3000);
server.Start();
