
namespace UiTest.Config
{
    public class TestMode
    {
        private readonly ModeConfig _config;

        public TestMode(string name, ModeConfig config)
        {
            Name = name;
            _config = config;
        }
        public readonly string Name;
        public ModeConfig Config => _config;
        public bool IsOnSFO => _config.IsOnSFO;
        public override string ToString() { return Name; }
    }
}
