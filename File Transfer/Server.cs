using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();

            //Socket Setup
            Socket server_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            int port = 8000;
            EndPoint end_point = new IPEndPoint(ip, port);

            server_socket.Bind(end_point);
            server_socket.Listen(100);
            Console.WriteLine("Listening at IP: {0}, Port: {1}", ip, port);

            Socket client_socket = server_socket.Accept();
            Console.WriteLine("Client Accepted");
            Console.WriteLine();

            //==================================================================================================

            Console.WriteLine("Enter File Path");
            string file_path = Console.ReadLine();

            string file_name = Path.GetFileName(file_path);
            string file_content = File.ReadAllText(file_path);

            byte[] file_name_length = BitConverter.GetBytes(file_name.Length);
            byte[] file_name_array = Encoding.ASCII.GetBytes(file_name);
            byte[] file_content_array = Encoding.ASCII.GetBytes(file_content);

            byte[] file_byte_array = new byte[1024];

            /*Byte Array Format*/
            //length of file name -> file name -> file content

            int index2 = file_name_length.Length;
            int index3 = file_name_length.Length + file_name_array.Length;

            file_name_length.CopyTo(file_byte_array, 0);
            file_name_array.CopyTo(file_byte_array, index2);
            file_content_array.CopyTo(file_byte_array, index3);

            Console.WriteLine();
            Console.WriteLine("Sending File: {0}", file_name);
            client_socket.Send(file_byte_array);
            Console.WriteLine("File Sent");
            Console.WriteLine();
        }
    }
}
