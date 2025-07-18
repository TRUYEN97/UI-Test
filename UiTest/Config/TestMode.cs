
using System.Windows.Media;
using UiTest.Service.CellService;

namespace UiTest.Config
{
    public class TestMode
    {
        private readonly ModeConfig _config;

        public TestMode(string name, ModeConfig config)
        {
            Name = name;
            _config = config;
            StandbyColor = Brushes.DarkCyan;
            ModeFlow = new ModeFlow();
        }
        public readonly string Name;
        public ModeConfig Config => _config;
        public bool IsOnSFO => _config.IsOnSFO;
        public Brush StandbyColor { get; private set; }
        public ModeFlow ModeFlow { get; internal set; }

        public override string ToString() { return Name; }
    }
}
