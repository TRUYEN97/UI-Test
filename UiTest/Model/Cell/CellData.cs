using UiTest.Config;
using UiTest.Service.Logger;

namespace UiTest.Model.Cell
{
    public class CellData
    {
        public readonly string Name;
        public readonly UnitLogger UnitLogger;
        public readonly TestData Model;
        public CellData(string name)
        {
            Name = name = name.ToUpper();
            UnitLogger = new UnitLogger(name);
            Model = new TestData(name);
        }
    }
}
