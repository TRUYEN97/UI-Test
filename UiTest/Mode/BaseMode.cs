using System.Collections.Generic;
using UiTest.Config;

namespace UiTest.Mode
{
    internal class BaseMode
    {
        private static readonly HashSet<string> ContainNames = new HashSet<string>();
        private static int index;
        public BaseMode(string name)
        {
            Name = name.ToUpper() ?? CreateName();
            ContainNames.Add(name);
        }
        public readonly string Name;
        public ModeConfig Config {  get; private set; }
        public bool IsOnSFO { get; private set; }
        public override string ToString() { return Name; }
        private static string CreateName()
        {
            string name = $"MODE-{++index}";
            while (ContainNames.Contains(name))
            {
                name = $"MODE-{++index}";
            }
            return name;
        }
    }
}
