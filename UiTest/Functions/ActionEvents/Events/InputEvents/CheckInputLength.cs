using System;
using System.Linq;
using System.Threading;
using UiTest.Common;
using UiTest.Functions.ActionEvents.Configs;
using UiTest.Model.Cell;

namespace UiTest.Functions.ActionEvents.Events.InputEvents
{
    public class CheckInputLength : BaseInputEvent<InputLengthConfig>
    {
        public CheckInputLength(InputLengthConfig config, CellData cellData) : base(config, cellData) { }

        protected override TestResult CheckInput(string input)
        {
            int length = input?.Length ?? 0;
            if (Config.LowerLimit <= length && length <= Config.UpperLimit)
            {
                //var mode = core.ModelManagement.Modes.Where(t => t.Name == "Production").ToArray()[0];
                //if (core.ModelManagement.UpdateMode(mode))
                //{
                //    CellData.TestMode = mode;
                //}
                return TestResult.PASSED;
            }
            return TestResult.FAILED;
        }
    }
}
