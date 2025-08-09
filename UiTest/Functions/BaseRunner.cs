using System;
using UiTest.Service.Logger;

namespace UiTest.Functions
{
    public abstract class BaseRunner<T, M> where T : CoverManagement<M>
    {
        protected T coverManagement;
        public bool IsRunning { get; protected set; }
        protected abstract bool IsRunnable();
        public event Action CancelRunEvent { add { coverManagement.CancelRunEvent += value; } remove { coverManagement.CancelRunEvent -= value; } }
        public void Run()
        {
            if (!IsRunnable()) return;
            try
            {
                IsRunning = true;
                coverManagement.Reset();
                RunAction();
            }
            catch (Exception ex)
            {
                ProgramLogger.AddError(GetType().Name, ex.Message);
            }
            finally
            {
                IsRunning = false;
            }
        }
        public void CancelRun()
        {
            coverManagement.CancelAllTask();
        }
        public bool IsPassed => coverManagement.IsPass;
        protected abstract void RunAction();
    }
}
