using System;
using System.Collections.ObjectModel;
using UiTest.Config;
using UiTest.Config.Events;

namespace UiTest.ModelView.TabItemViewModel.SettingTabs
{
    public class ActionEventSettingModelView : BaseTabSettingModelView
    {
        private readonly ActionEvents actionEvent;
        private BaseTabSettingModelView _selectedTab;
        private int _selectedIndex;
        public ActionEventSettingModelView(ActionEvents actionEvent) : base("Events")
        {
            this.actionEvent = actionEvent;
            Tabs = new ObservableCollection<BaseTabSettingModelView>();
            Reload();
        }
        public ObservableCollection<BaseTabSettingModelView> Tabs { get; }
        public BaseTabSettingModelView SelectedTab { get => _selectedTab; set { _selectedTab = value; OnPropertyChanged(); } }
        public int SelectedIndex { get => _selectedIndex; set { _selectedIndex = value; OnPropertyChanged(); } }
        public override void Reload()
        {
            int tabIndex = SelectedIndex;
            Tabs.Clear();
            Tabs.Add(new ShowEventSettingModelView(actionEvent.ActionTools, "Tools"));
            Tabs.Add(new ShowEventSettingModelView(actionEvent.LauchEvents, "Lauch"));
            Tabs.Add(new ShowEventSettingModelView(actionEvent.WindownClosingEvents, "Windown closing"));
            SelectedIndex = Tabs.Count > tabIndex ? tabIndex : 0;
        }

        public override void Save()
        {
            foreach (var item in Tabs)
            {
                item.Save();
            }
        }
    }
}
