
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using UiTest.Common;
using UiTest.Config.Events;
using UiTest.Service.CellService;

namespace UiTest.Config
{
    public class TestMode
    {
        private readonly ModeConfig _config;
        private readonly ProgramConfig _programConfig;
        public TestMode(string name, ModeConfig config, ProgramConfig programConfig)
        {
            Name = name;
            _config = config;
            _programConfig = programConfig;
        }
        public readonly string Name;
        public Dictionary<string, string> Properties
        {
            get
            {
                var prt = new Dictionary<string, string>();
                _programConfig.ProgramSetting.Properties.Any(i => { prt[i.Key] = i.Value; return false; });
                _config.Properties.Any(i => { prt[i.Key] = i.Value; return false; });
                return prt;
            }
        }
        public ModeConfig Config => _config;
        public Brush StandbyColor => Util.GetBrushFromString(_config.StandbyColor, Brushes.DarkCyan);
        public List<ActionEventSetting> InputEvents => new List<ActionEventSetting>(_config.InputEvents);
        public List<ActionEventSetting> ModeChangeEvents => new List<ActionEventSetting>(_config.ModeChangeEvents);
        public ModeFlow ModeFlow => new ModeFlow(_config, _programConfig, Name);
        public int Loop => _config.LoopTimes;
        public override string ToString() { return Name; }
    }
}
