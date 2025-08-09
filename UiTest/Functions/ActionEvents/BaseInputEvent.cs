using System;
using UiTest.Functions.Interface;
using UiTest.Model.Cell;
using UiTest.Service.Logger;
using UiTest.Service;
using UiTest.Common;

namespace UiTest.Functions.ActionEvents.Events.InputEvents
{
    public abstract class BaseInputEvent<T> : BaseFunction<T>, IInputEvent
    {
        protected readonly Core core;
        public CellData CellData { get; protected set; }
        protected BaseInputEvent(T config, CellData cellData) : base(config)
        {
            CellData = cellData;
            core = Core.Instance;
        }

        public object BaseConfig => BaseConfig;

        public bool IsCancelled => Cts.IsCancellationRequested;

        public override void Run()
        {
            try
            {
                Result = CheckInput(CellData.Input);
            }
            catch (Exception ex)
            {
                ProgramLogger.AddError(nameof(this.GetType), ex.Message);
            }
        }
        protected abstract TestResult CheckInput(string input);
    }
}
