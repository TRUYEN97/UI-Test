using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UiTest.Service.Communicate.InOutStream;
using StringWriter = UiTest.Service.Communicate.InOutStream.StringWriter;

namespace UiTest.Service.Communicate.Implement.SocketSv.Client
{
    public class ClientSocket : BaseCommunicateRunner
    {
        private readonly Socket socket;
        public IPEndPoint IPEndPoint { get; set; }
        public ClientSocket() : this(new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)) { }
        public ClientSocket(Socket socket)
        {
            this.socket = socket;
            NetworkStream stream = new NetworkStream(socket, true);
            OutPutReader = new StringStreamReader(new StreamReader(stream, Encoding.UTF8));
            InputWriter = new StringWriter(new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true});
        }

        public override bool Connect()
        {
            try
            {
                if (IPEndPoint == null) return false;
                socket.Connect(IPEndPoint);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override bool Disconnect()
        {
            try
            {
                socket.Disconnect(true);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override bool IsConnect()
        {
            return socket?.Connected == true;
        }

        protected override void Close()
        {
            try
            {
                socket?.Dispose();
            }
            catch (Exception)
            {
            }
        }
    }
}
