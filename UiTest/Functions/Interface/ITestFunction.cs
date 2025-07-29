using System;
using System.Threading;
using UiTest.Common;
using UiTest.Functions.Config;
using UiTest.Model.Cell;
using UiTest.Model.Function;

namespace UiTest.Functions.Interface
{
    public interface ITestFunction
    {
        CancellationTokenSource Cts { get; set; }
        BasefunctionConfig BaseConfig { get; }
        FunctionData FunctionData { get; }
        void Cancel();
        (TestStatus status, string value) Run(int times);
    }
}
