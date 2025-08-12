using System.Collections.Generic;
using System.Linq;
using System.Windows;
using UiTest.Config;
using UiTest.Config.Events;
using UiTest.ModelView.Component;
using UiTest.ModelView.ListBoxItems;
using UiTest.Service.Factory;

namespace UiTest.ModelView.TabItemViewModel.SettingTabs
{
    public class ShowEventSettingModelView : BaseTabSettingModelView
    {
        private List<ActionEventSetting> actionTools;
        private object _selectedItem;

        public ShowEventSettingModelView(List<ActionEventSetting> actionTools, string title) : base(title)
        {
            this.actionTools = actionTools;
            ActionListModelView = new ListBoxModelView("Actions");
            TreeViewModelView = new TreeViewModelView();
            Reload();
        }
        public ListBoxModelView ActionListModelView { get; }
        public TreeViewModelView TreeViewModelView { get; }

        public override void Reload()
        {
            TreeViewModelView.BuildTree(ActionEventFatory.Instance.Types);
            ActionListModelView.Clear();
            if (actionTools != null)
            {
                var names = ActionEventFatory.Instance.ListName;
                PropertySettingModelView item;
                foreach (var action in actionTools)
                {
                    if (names.Contains(action.FunctionType))
                    {
                        item = new PropertySettingModelView(action.Name, action.FunctionType, ActionListModelView.Items)
                        {
                            IsValueReadOnly = true,
                            Tag = action.Config,
                            IconPath = "/Resources/EditIcon.png",
                        };
                        item.ClickEvent += i => SelectedItem = i.Tag;
                        item.DeleteEvent += i => { if (SelectedItem == i.Tag) SelectedItem = null; };
                        ActionListModelView.Add(item);
                    }
                }
            }
        }

        public object SelectedItem { get => _selectedItem; set { _selectedItem = value; OnPropertyChanged(); } }

        public override void Save()
        {
            var names = ActionEventFatory.Instance.ListName;
            actionTools = actionTools ?? new List<ActionEventSetting>();
            actionTools.Clear();
            foreach (var item in ActionListModelView.Items)
            {
                if (item is PropertySettingModelView settingModelView
                    && !string.IsNullOrWhiteSpace(settingModelView.Name))
                {
                    string type = settingModelView.Value;
                    if (names.Contains(type))
                    {
                        actionTools.Add(new ActionEventSetting
                        {
                            Name = settingModelView.Name,
                            FunctionType = type,
                            Config = settingModelView.Tag
                        });
                    }
                    else
                    {
                        MessageBox.Show($"{type} does not exist in the action type list!");
                    }
                }
            }
        }
    }
}
