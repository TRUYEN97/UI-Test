using System;
using System.Threading.Tasks;
using UiTest.Config.Events;
using UiTest.Functions.Interface;
using UiTest.Model.Cell;

namespace UiTest.Functions.ActionEvents.Events.InputEvents
{
    public abstract class BaseInputEvent<T> : BaseActionEvent<T>, IInputEvent
    {
        public BaseInputEvent(T config, CellData cellData) : base(config)
        {
            CellData = cellData;
        }
        public CellData CellData { get; protected set; }
        protected override bool Test()
        {
            return CheckInput(CellData.Input);
        }
        protected abstract bool CheckInput(string input);
    }
}
