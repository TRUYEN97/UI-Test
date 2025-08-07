using System.Threading;

namespace UiTest.Functions.Interface
{
    public interface IFucntion<T>
    {
        bool IsAcceptable { get; }
        T BaseConfig { get; }
        CancellationTokenSource Cts { get; set; }
        bool IsCanceled { get; }
        void Cancel();
        void Run();
        void Stop();
    }
}
