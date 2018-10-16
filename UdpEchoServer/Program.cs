using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UdpEchoServer
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a UdpClient for reading incoming data 
           UdpClient udpServerClient = new UdpClient(9999);

            // Create an IPEndPoint to record the IP and port of sender
            IPAddress senderIpAddress = IPAddress.Loopback;
            // sender port
            int senderPort = 9999;
            IPEndPoint remoteIpEndPoint = new IPEndPoint(senderIpAddress, senderPort);

            Console.WriteLine("Hello World! Here is UDP Server!");

            try
            {
                // Block until a message is received from this socket  from a remote host ( client)
                Console.WriteLine("Server is Blocked");
                while (true)
                {
                    Byte[] receivedBytes = udpServerClient.Receive(ref remoteIpEndPoint);
                    Console.WriteLine("Now server is activated");
                    string receivedMsg = Encoding.ASCII.GetString(receivedBytes);
                    Console.WriteLine($"Received Message from Client: {receivedMsg}");
                    string[] data = receivedMsg.Split(' ');
                    string clientName = data[0];
                    string text = data[1] + data[2];

                    // server send back data to client 
                    string sendData = $"Server : {text}";
                    Byte[] sendBytes = Encoding.ASCII.GetBytes(sendData);
                    udpServerClient.Send(sendBytes, sendBytes.Length, remoteIpEndPoint);
                    Console.WriteLine($"The Message was sent by IP:{remoteIpEndPoint.Address} on their Port :{remoteIpEndPoint.Port}");
                   
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            

           

        }
    }
}
