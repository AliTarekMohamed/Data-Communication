using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;
using System.IO;

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

            /*Byte Array Format*/
            //length of file name -> file name -> file content

            byte[] file_byte_array = new byte[1024];
            int file_length = client_socket.Receive(file_byte_array);   //Total file length

            int file_name_length;
            string file_name;
            string file_content;

            //Decode byte array
            file_name_length = BitConverter.ToInt32(file_byte_array, 0);
            file_name = Encoding.ASCII.GetString(file_byte_array, sizeof(int), file_name_length);
            file_content = Encoding.ASCII.GetString(file_byte_array, (sizeof(int) + file_name_length), (file_length - (sizeof(int) + file_name_length)));

            //Create file in client side
            string copied_file_path = @"D:\FCIS - ASU\Y3S2\Data Communication\Assignment 1\Client\" + file_name;
            FileStream copied_file = File.Create(copied_file_path);
            copied_file.Write(file_byte_array, (sizeof(int) + file_name_length), file_content.Length);

            Console.WriteLine("File name length: {0}", file_name_length);
            Console.WriteLine("File name: {0}", file_name);
            Console.WriteLine("File content: {0}", file_content);
        }
    }
}