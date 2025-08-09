using System.Windows.Input;
using UiTest.Config.Events;
using UiTest.Service.Factory;
using UiTest.Service.Relay;

namespace UiTest.ModelView.ListBoxItems
{
    public class ActionToolModelView : BaseModelView
    {
        public ActionToolModelView(ActionEventSetting actionEventSetting)
        {
            Text = actionEventSetting?.Name;
            var actionEvent = ActionEventFatory.Instance.CreateFunctionWith(actionEventSetting);
            Attack = new RelayCommand( _ => actionEvent?.Run(), _ => !string.IsNullOrWhiteSpace(Text));
        }
        public string Text { get; private set; }
        public ICommand Attack { get; private set; }
    }
}
