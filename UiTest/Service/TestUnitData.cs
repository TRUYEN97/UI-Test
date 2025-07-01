
using UiTest.Model;

namespace UiTest.Service
{
    internal class TestUnitData
    {
        public readonly string Name;
        public readonly UnitLogger UnitLogger;
        public readonly TestUnitModel Model;
        public TestUnitData(string name)
        {
            Name = name;
            UnitLogger = new UnitLogger();
            Model = new TestUnitModel();
        }
    }
}
