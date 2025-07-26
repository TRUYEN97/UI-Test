using System;
using System.Threading;
using UiTest.Common;
using UiTest.Functions.Config;
using UiTest.Functions.Config.Sfis;
using UiTest.Model.Function;

namespace UiTest.Functions.Body.Sfis
{
    public class SendSfis : BaseFunctionBody<SfisConfig>
    {
        public SendSfis(FunctionData functionData) : this(functionData, null) { }
        public SendSfis(FunctionData functionData, SfisConfig config) : base(functionData, config) { }

        protected override (ItemStatus status, string value) Test()
        {
            Logger.AddInfoText($"Param1: {Config.Param1}");
            Logger.AddInfoText($"Param2: {Config.Param2}");
            Logger.AddInfoText($"Param3: {Config.Param3}");
            Thread.Sleep(5000);
            Logger.AddInfoText("gggggg");
            return ((Config.Param3 == 1? ItemStatus.PASSED : ItemStatus.FAILED),"");
        }
    }
}
