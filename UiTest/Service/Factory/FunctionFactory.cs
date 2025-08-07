using System;
using UiTest.Functions.Interface;
using UiTest.Model.Function;
using UiTest.Config.Items;
using UiTest.Functions.TestFunctions.Config;

namespace UiTest.Service.Factory
{
    public class FunctionFactory : BaseFactory<ITestFunction>
    {
        private static readonly Lazy<FunctionFactory> _insatnce = new Lazy<FunctionFactory>(() => new FunctionFactory());
        public static FunctionFactory Instance = _insatnce.Value;
        public ITestFunction CreateFunctionWith(string typeName, BasefunctionConfig basefunctionConfig, FunctionData functionData, ItemSetting itemSetting)
        {
            return CreateInstanceWithTypeName(typeName, basefunctionConfig, functionData, itemSetting);
        }
        public ITestFunction CreateFunctionWith(FunctionConfig functionConfig, FunctionData functionData)
        {
            return CreateInstanceWithTypeName(functionConfig.FunctionType, functionConfig.Config, functionData,  functionConfig.ItemSetting);
        }
    }
}
