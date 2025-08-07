using System;
using UiTest.Config.Events;
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

        public bool IsAcceptable { get; private set; }

        public object BaseConfig => Config;

        public override void Run()
        {
            try
            {
                IsAcceptable = Test();
            }
            catch (Exception ex)
            {
                ProgramLogger.AddError(nameof(this.GetType), ex.Message);
            }
        }

        protected abstract bool Test();
    }
}
