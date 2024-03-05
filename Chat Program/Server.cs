using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Socket Setup
            Console.Title = "Server";

            Socket server_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            EndPoint end_point = new IPEndPoint(IPAddress.Any, 8000);

            server_socket.Bind(end_point);
            server_socket.Listen(100);

            Socket client_socket = server_socket.Accept();

            //==================================================================================================

            string client_message = "";
            string server_message = "";
            byte[] message_byte_array = message_byte_array = new byte[1024];

            while (client_message.ToLower() != "bye" || server_message.ToLower() != "bye")
            {
                //Send
                Console.Write("You : ");
                server_message = Console.ReadLine();

                message_byte_array = Encoding.ASCII.GetBytes(server_message);
                client_socket.Send(message_byte_array);

                //==================================================================
                //Recieve
                message_byte_array = new byte[1024];
                int message_length = client_socket.Receive(message_byte_array);
                client_message = Encoding.ASCII.GetString(message_byte_array, 0, message_length);
                Console.WriteLine("Client : {0}", client_message);
            }
        }
    }
}