
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
            StandbyColor = Util.GetBrushFromString(config.StandbyColor, Brushes.DarkCyan);
        }
        public readonly string Name;
        public ModeConfig Config => _config;
        public bool IsOnSFO => _config.IsOnSFO;
        public Brush StandbyColor { get; private set; }
        public ModeFlow ModeFlow => new ModeFlow(_config, _programConfig);

        public int Loop => _config.LoopTimes;

        public override string ToString() { return Name; }
    }
}
