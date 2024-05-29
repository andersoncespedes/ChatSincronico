using Cliente;
// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");
Client e = new("localhost", 3000);
e.Start();
string user;
string password;
User user1;
while (true)
{
    Console.Write("Escribe un Usuario -> ");
    user = Console.ReadLine();

    Console.Write("Escribe un Contraseña -> ");
    password = Console.ReadLine();
    user1 = new(user, password);
    e.SendObject(user1);
    Console.WriteLine(e.Receive());
}

