using System;
using System.Collections.ObjectModel;
using UiTest.Config;
using UiTest.Service.Interface;
using UiTest.Service.Logger;

namespace UiTest.Service.Managements
{
    public class ModelManagement:IUpdate
    {
        private readonly ProgramConfig programConfig;
        private readonly ObservableCollection<TestMode> _modes;
        private readonly CellManagement cellManagement;
        private TestMode _selectedMode;

        public ModelManagement(ProgramConfig programConfig, CellManagement cellManagement) { 
            _modes = new ObservableCollection<TestMode>();
            this.programConfig = programConfig;
            this.cellManagement = cellManagement;
        }
        public ObservableCollection<TestMode> Modes => _modes;
        public TestMode SelectedMode { get => _selectedMode; set => UpdateMode(value); }
        public bool Update()
        {
            try
            {
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
                ProgramLogger.AddError("Mode Management" ,ex.Message);
                return false;
            }
        }

        public bool UpdateMode(TestMode mode) {
            if (mode == null) return false;
            _selectedMode = mode;
            cellManagement.UpdataMode(_selectedMode);
            return true;
        }
    }
}
