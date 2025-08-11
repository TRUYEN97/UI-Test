using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json.Linq;
using UiTest.Common;
using UiTest.Config;
using UiTest.Functions.ActionEvents;
using UiTest.Model;
using UiTest.ModelView.ListBoxItems;
using UiTest.Service.Interface;
using UiTest.Service.Logger;

namespace UiTest.Service.Managements
{
    public class ModelManagement : IUpdate
    {
        private readonly ProgramConfig programConfig;
        private readonly ObservableCollection<TestMode> _modes;
        private readonly CellManagement cellManagement;
        private readonly ActionEventRunner actionEventRunner;
        private TestMode _selectedMode;

        public ModelManagement(ProgramConfig programConfig, CellManagement cellManagement)
        {
            _modes = new ObservableCollection<TestMode>();
            this.programConfig = programConfig;
            this.cellManagement = cellManagement;
            Properties = new ObservableCollection<PropertyModelView>();
            actionEventRunner = new ActionEventRunner();
        }
        public ObservableCollection<TestMode> Modes => _modes;
        public TestMode SelectedMode => _selectedMode;

        public ObservableCollection<PropertyModelView> Properties { get; private set; }
        public event Action OnSelectedModeChanged;
        public bool Update()
        {
            try
            {
                Clear();
                if (programConfig?.Modes == null)
                {
                    return false;
                }
                foreach (var modeConfig in programConfig.Modes)
                {
                    Modes.Add(new TestMode(modeConfig.Key, modeConfig.Value, programConfig));
                }
                return UpdateMode(Modes[0]);
            }
            catch (Exception ex)
            {
                ProgramLogger.AddError("Mode Management", ex.Message);
                return false;
            }
        }

        private void Clear()
        {
            Modes.Clear();
            _selectedMode = null;
            DispatcherUtil.RunOnUI(() =>
            {
                OnSelectedModeChanged?.Invoke();
            });
        }

        public bool UpdateMode(TestMode mode)
        {
            try
            {
                if (mode == null || actionEventRunner.IsRunning)
                {
                    return false;
                }
                actionEventRunner.ActionEvents = mode.ModeChangeEvents;
                actionEventRunner.Run();
                if (!actionEventRunner.IsPassed)
                {
                    return false;
                }
                DispatcherUtil.RunOnUI(() =>
                {
                    if (cellManagement.Count == 1 || _selectedMode == null)
                    {
                        cellManagement.UpdataMode(mode);
                    }
                    _selectedMode = mode;
                    UpdateProperties(mode.Properties);
                });
                return true;
            }
            finally
            {
                DispatcherUtil.RunOnUI(() =>
                {
                    OnSelectedModeChanged?.Invoke();
                });
            }
        }

        private void UpdateProperties(Dictionary<string, string> properties)
        {
            Properties.Clear();
            properties?.Any(i => { Properties.Add(new PropertyModelView(i.Key, i.Value)); return false; });
        }
    }
}
