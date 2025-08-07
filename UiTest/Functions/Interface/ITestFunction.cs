using UiTest.Config.Items;
using UiTest.Functions.TestFunctions.Config;
using UiTest.Model.Function;

namespace UiTest.Functions.Interface
{
    public interface ITestFunction: IFucntion<BasefunctionConfig>
    {
        ItemSetting ItemSetting { get; }
        FunctionData FunctionData { get; }
    }
}
