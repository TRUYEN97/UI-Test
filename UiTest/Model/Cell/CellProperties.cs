using UiTest.Config;

namespace UiTest.Model.Cell
{
    public class CellProperties
    {
        public readonly ProgramInfo ProgramInfo;
        public CellProperties(string name) 
        {
            Name = name;
            ProgramInfo = ConfigLoader.ProgramConfig.ProgramInfo;
        }
        public string Name { get; private set; }
    }
}
