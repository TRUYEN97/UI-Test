using System;
using System.Threading;
using UiTest.Config.Events;
using UiTest.Functions.Interface;
using UiTest.Model.Cell;
using UiTest.Service.Factory;

namespace UiTest.Functions.ActionEvents
{
    public class InputEventRunner : ActionEventRunner
    {
        private readonly CellData cellData;

        public InputEventRunner(CellData cellData):base()
        {
            this.cellData = cellData;
        }
        protected override IActionEvent GetFunctionBody(ActionEventSetting actionEvents)
        {
            return InputEventFactory.Instance.CreateInputActionWith(actionEvents, cellData);
        }
    }
}
