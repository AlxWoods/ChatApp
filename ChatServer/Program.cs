// See https://aka.ms/new-console-template for more information

using System;
using System.Net;
using System.Net.Sockets;

namespace ChatServer
{

    class Program 
    {
        static List<Client> _users;
        static TcpListener _listener;
        static void Main(string[] args) 
        {
            _users = new List<Client>();
            _listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 30005);
            _listener.Start();

            while (true)
            {
                var client = new Client(_listener.AcceptTcpClient());
                _users.Add(client);

                /* Boradcast the connection*/
            }


        }
    }

}