using System.Threading;
using System.Threading.Tasks;

namespace UiTest.Functions
{
    public abstract class BaseRunner<T, M> where T : CoverManagement<M>
    {
        protected T coverManagement;
        public bool IsRunning {  get; protected set; }
        protected abstract bool IsRunnable();
        public void Run()
        {
            if (!IsRunnable()) return;
            try
            {
                IsRunning = true;
                coverManagement.Cts = new CancellationTokenSource();
                RunAction();
            }
            finally
            {
                IsRunning = false;
            }
        }
        protected abstract void RunAction();
    }
}
