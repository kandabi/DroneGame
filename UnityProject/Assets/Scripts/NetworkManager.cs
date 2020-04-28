using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

public class NetworkManager 
{
    //Simple UDP client/server
    public struct Received
    {
        public IPEndPoint Sender;
        public string Message;
        public byte[] bytes;
    }

    public abstract class UdpBase
    {
        protected UdpClient Client;

        protected UdpBase()
        {
            Client = new UdpClient();
        }

        public async Task<Received> Receive()
        {
            var result = await Client.ReceiveAsync();
            return new Received()
            {
                bytes = result.Buffer.ToArray(),
                Message = Encoding.ASCII.GetString(result.Buffer, 0, result.Buffer.Length),
                Sender = result.RemoteEndPoint
            };
        }
    }

    //Server
    public class UdpListener : UdpBase
    {
        private IPEndPoint listenOn;

        public UdpListener(int port, string address = "0.0.0.0")
        {
            Client = new UdpClient(new IPEndPoint(IPAddress.Parse(address), port));
        }
    }

    //Client
    public class UdpSender : UdpBase
    {
        public void ConnectTo(string hostname, int port)
        {
            Client.Connect(hostname, port);
        }

        public int Send(string message)
        {
            var datagram = Encoding.ASCII.GetBytes(message);
            Client.Send(datagram, datagram.Length);
            return datagram.Length;
        }
        public int Send(byte[] message)
        {
            Client.Send(message, message.Length);
            return message.Length;
        }
    }
}
