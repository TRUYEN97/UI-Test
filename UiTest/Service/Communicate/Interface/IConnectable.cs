

namespace UiTest.Service.Communicate.Interface
{
    internal interface IConnectable
    {
        bool Connect();
        bool IsConnect();
        bool Disconnect();
    }
}
