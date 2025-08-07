using System;
using UiTest.Config.Events;
using UiTest.Functions.Interface;
using UiTest.Model.Cell;

namespace UiTest.Service.Factory
{
    public class InputEventFactory: BaseFactory<IInputEvent>
    {
        private static readonly Lazy<InputEventFactory> _insatnce = new Lazy<InputEventFactory>(() => new InputEventFactory());
        public static InputEventFactory Instance = _insatnce.Value;
        private InputEventFactory() { }
        public IInputEvent CreateInputActionWith(ActionEventSetting inputEvent, CellData cellData)
        {
            return CreateInstanceWithTypeName(inputEvent.FunctionType, inputEvent.Config, cellData);
        }
    }
}
