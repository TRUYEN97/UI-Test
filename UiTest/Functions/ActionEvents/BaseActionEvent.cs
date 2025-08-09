using System;
using UiTest.Common;
using UiTest.Functions.Interface;
using UiTest.Service;
using UiTest.Service.Logger;

namespace UiTest.Functions.ActionEvents
{
    public abstract class BaseActionEvent<T> : BaseFunction<T>, IActionEvent
    {
        protected readonly Core core;
        protected BaseActionEvent(T config) : base(config)
        {
            core = Core.Instance;
        }

        public object BaseConfig => BaseConfig;

        public bool IsCancelled => Cts.IsCancellationRequested;

        public override void Run()
        {
            try
            {
                Result = Test();
            }
            catch (Exception ex)
            {
                ProgramLogger.AddError(GetType().Name, ex.Message);
            }
        }

        protected abstract TestResult Test();
    }
}
