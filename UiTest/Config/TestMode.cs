
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using UiTest.Common;
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
        public bool IsOnSFO => _config.IsOnSFO;
        public Brush StandbyColor => Util.GetBrushFromString(_config.StandbyColor, Brushes.DarkCyan);
        public Brush PassColor => Util.GetBrushFromString(_config.PassColor, Brushes.LightGreen);
        public Brush CancelColor => Util.GetBrushFromString(_config.CancelColor, Brushes.Orange);
        public ModeFlow ModeFlow => new ModeFlow(_config, _programConfig, Name);
        public int Loop => _config.LoopTimes;
        public override string ToString() { return Name; }
    }
}
