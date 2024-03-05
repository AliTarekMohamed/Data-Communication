using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Client";

            //Socket Setup
            Socket client_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            EndPoint end_point = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);

            client_socket.Connect(end_point);

            //=================================================================================================

            string client_message = "";
            string server_message = "";
            byte[] message_byte_array = new byte[2048];

            while (client_message.ToLower() != "bye" || server_message.ToLower() != "bye")
            {
                //Recieve
                message_byte_array = new byte[1024];
                int message_length = client_socket.Receive(message_byte_array);
                server_message = Encoding.ASCII.GetString(message_byte_array, 0, message_length);
                Console.WriteLine("Server : {0}", server_message);

                //==================================================================
                //Send
                Console.Write("You : ");
                client_message = Console.ReadLine();

                message_byte_array = Encoding.ASCII.GetBytes(client_message);
                client_socket.Send(message_byte_array);
                
            }
        }
    }
}