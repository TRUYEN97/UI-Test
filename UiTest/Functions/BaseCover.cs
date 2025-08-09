using System;
using System.Threading;
using UiTest.Common;
using UiTest.Functions.Interface;
namespace UiTest.Functions
{
    public abstract class BaseCover<T>: IFunction<T>
    {
        protected bool isRunning;
        protected readonly IFunction<T> functionBody;
        protected readonly CoverManagement<T> coverManagement;
        protected BaseCover(IFunction<T> functionBody, CoverManagement<T> coverManagement)
        {
            this.functionBody = functionBody;
            this.coverManagement = coverManagement;
        }
        public virtual void Cancel()
        {
            functionBody.Cancel();
        }
        public virtual CancellationTokenSource Cts { get; set; }
        public virtual bool IsRunning => isRunning;
        public TestResult Result => functionBody.Result;
        public T BaseConfig => functionBody.BaseConfig;
        public bool IsCancelled => functionBody.IsCancelled;
        public abstract void Run();
    }
}
