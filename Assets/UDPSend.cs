using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace UDPSend
{
    public class UDPSender {

        private static UDPSender instance;
        private static int localPort;
        private string IP; 
        public int port;
        IPEndPoint remoteEndPoint;
        UdpClient client;

        private UDPSender()
        {
            Debug.Log("UDPSend.init()");
            IP = "127.0.0.1";
            port = 4445;
            remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), port);
            client = new UdpClient();
        }

        // sendData
        public void sendString(string message)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                client.Send(data, data.Length, remoteEndPoint);
            }
            catch (Exception err)
            {
                Debug.Log(err.ToString());
            }
        }

        public static UDPSender getInstance()
        {
            if(instance == null)
            {
                instance = new UDPSender();
            }
            return instance;
        }
    }
}