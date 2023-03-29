using ChatServer.Net.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ChatServer
{
    internal class Client
    {
        public string Username { get; set; }
        public Guid UID { get; set; }
        public TcpClient ClientSocket { get; set; }
        PacketReader _packetReader;
        public Client(TcpClient client) 
        {
            ClientSocket = client;
            UID = Guid.NewGuid();
            _packetReader = new PacketReader(ClientSocket.GetStream());

            var opcode = _packetReader.ReadByte();
            Username= _packetReader.ReadMessage();

            Console.WriteLine($"[{DateTime.Now}]: Client user: {Username}");
        }

        void Process()
        {
            while(true)
            {
                try 
                { 
                    var opcode = _packetReader.ReadByte() ;
                    switch (opcode) 
                    {
                        case 5:
                            var msg = _packetReader.ReadMessage() ;
                            Console.WriteLine($"[{DateTime.Now}]: Message recieved {msg}");
                            Program.BroadcastMessage(msg);
                            break;
                        default: break;
                    }
                }
                catch( Exception e )
                {
                    Console.WriteLine($"[{UID.ToString()}]: Disconnected");
                    ClientSocket.Close();
                    throw;
                }
            }
        }
    }
}
