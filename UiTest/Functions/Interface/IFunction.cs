using System.Threading;
using UiTest.Common;

namespace UiTest.Functions.Interface
{
    public interface IFunction<T>
    {
        T BaseConfig { get; }
        CancellationTokenSource Cts { get;}
        TestResult Result { get; }
        bool IsCancelled { get; }
        void Cancel();
        void Run();
    }
}
