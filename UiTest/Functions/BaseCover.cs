using System;
using System.Threading;
using System.Threading.Tasks;
using UiTest.Functions.Interface;
namespace UiTest.Functions
{
    public abstract class BaseCover<T>
    {
        protected bool isRunning;
        protected readonly IFucntion<T> functionBody;
        protected readonly CoverManagement<T> coverManagement;
        protected BaseCover(IFucntion<T> functionBody, CoverManagement<T> coverManagement)
        {
            this.functionBody = functionBody;
            this.coverManagement = coverManagement;
        }
        public virtual void Stop()
        {
            functionBody.Stop();
        }

        public virtual void Cancel()
        {
            functionBody.Cancel();
        }
        public virtual CancellationTokenSource Cts { get; set; }
        public virtual bool IsCanceled => Cts?.IsCancellationRequested == true;
        public virtual bool IsRunning => isRunning;
        public virtual bool IsAcceptable { get; protected set; }
        public abstract void Run();
    }
}
