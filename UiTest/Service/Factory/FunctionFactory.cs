using System;
using UiTest.Functions.Config;
using UiTest.Functions.Interface;
using UiTest.Model.Function;

namespace UiTest.Service.Factory
{
    public class FunctionFactory : BaseFactory<ITestFunction>
    {
        private static readonly Lazy<FunctionFactory> _insatnce = new Lazy<FunctionFactory>(() => new FunctionFactory());
        public static FunctionFactory Instance = _insatnce.Value;
        public ITestFunction CreateFunctionWithTypeName(string typeName, FunctionData functionData, BasefunctionConfig basefunctionConfig)
        {
            return CreateInstanceWithTypeName(typeName, functionData, basefunctionConfig);
        }
    }
}
