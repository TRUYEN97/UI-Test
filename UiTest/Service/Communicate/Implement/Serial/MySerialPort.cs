using System;
using System.IO;
using System.IO.Ports;
using System.Text;
using UiTest.Service.Communicate.InOutStream;
using UiTest.Service.Communicate.Interface;
using StringWriter = UiTest.Service.Communicate.InOutStream.StringWriter;

namespace UiTest.Service.Communicate.Implement.Serial
{
    public class MySerialPort : BaseCommunicate
    {
        private readonly SerialPort serialPort;
        public MySerialPort(string name, int baudrate) : this(name, baudrate, null) { }
        public MySerialPort(string name, int baudrate, Action<IWriter,string> dataReceivedAction)
        {
            serialPort = new SerialPort(name, baudrate)
            {
                Parity = Parity.None,
                StopBits = StopBits.One,
                DataBits = 8,
                Handshake = Handshake.None,
                Encoding = Encoding.UTF8
            };
            DynamicTextReader outPutReader = new DynamicTextReader();
            OutPutReader = outPutReader;
            serialPort.DataReceived += (_, o) =>
            {
                try
                {
                    string line = serialPort.ReadLine()?.Trim();
                    dataReceivedAction?.Invoke(InputWriter,line);
                    outPutReader.AddLine(line);
                }
                catch (Exception)
                {
                    dataReceivedAction.Invoke(InputWriter, null);
                    outPutReader.AddLine(null);
                }
            };
        }

        protected override void Close()
        {
            serialPort?.Dispose();
        }

        public static string[] GetPortNames()
        {
            return SerialPort.GetPortNames();
        }

        public override bool Connect()
        {
            try
            {
                if (!serialPort.IsOpen)
                {
                    serialPort.Open();
                    InputWriter = new StringWriter(new StreamWriter(serialPort.BaseStream, Encoding.UTF8, 2048, leaveOpen: true)
                    {
                        AutoFlush = true
                    });
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override bool IsConnect()
        {
            return serialPort.IsOpen;
        }

        public override bool Disconnect()
        {
            try
            {
                if (serialPort.IsOpen)
                {
                    serialPort.Close();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
