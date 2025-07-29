using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UiTest.Config;
using UiTest.Model;
using UiTest.Service.Interface;
using UiTest.Service.Logger;

namespace UiTest.Service.Managements
{
    public class ModelManagement : IUpdate
    {
        private readonly ProgramConfig programConfig;
        private readonly ObservableCollection<TestMode> _modes;
        private readonly CellManagement cellManagement;
        private TestMode _selectedMode;

        public ModelManagement(ProgramConfig programConfig, CellManagement cellManagement)
        {
            _modes = new ObservableCollection<TestMode>();
            this.programConfig = programConfig;
            this.cellManagement = cellManagement;
            Properties = new ObservableCollection<PropertyModel>();
        }
        public ObservableCollection<TestMode> Modes => _modes;
        public TestMode SelectedMode { get => _selectedMode; set => UpdateMode(value); }
        public ObservableCollection<PropertyModel> Properties { get; private set; }
        public bool Update()
        {
            try
            {
                Modes.Clear();
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
        public bool UpdateMode(TestMode mode)
        {
            if (mode == null)
            {
                return false;
            }
            _selectedMode = mode;
            if (cellManagement.Count == 1)
            {
                cellManagement.UpdataMode(_selectedMode);
            }
            UpdateProperties(mode.Properties);
            return true;
        }

        private void UpdateProperties(Dictionary<string, string> properties)
        {
            Properties.Clear();
            properties?.Any(i => { Properties.Add(new PropertyModel(i.Key, i.Value)); return false; });
        }
    }
}
