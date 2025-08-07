using System;
using UiTest.Config.Events;
using UiTest.Functions.Interface;

namespace UiTest.Service.Factory
{
    public class ActionEventFatory : BaseFactory<IActionEvent>
    {
        private static readonly Lazy<ActionEventFatory> _insatnce = new Lazy<ActionEventFatory>(() => new ActionEventFatory());
        public static ActionEventFatory Instance = _insatnce.Value;
        private ActionEventFatory() { }
        public IActionEvent CreateFunctionWith(ActionEventSetting inputEvent)
        {
            return CreateInstanceWithTypeName(inputEvent.FunctionType, inputEvent.Config);
        }
    }
}
