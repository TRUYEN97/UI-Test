using System;
using UiTest.Common;
using UiTest.Functions.Interface;
using UiTest.Model.Cell;
using UiTest.Model.Function;
using UiTest.Service.Logger;
using UiTest.Config.Items;
using UiTest.Functions.TestFunctions.Config;
using System.Threading.Tasks;

namespace UiTest.Functions.TestFunctions.Body
{
    public abstract class BaseFunctionBody<T> : BaseFunction<T>, ITestFunction where T : BasefunctionConfig
    {
        private readonly FunctionData _functionData;
        private readonly CellData cellData;
        protected BaseFunctionBody(FunctionData functionData) : this(null, functionData, new ItemSetting()) { }
        protected BaseFunctionBody(T config, FunctionData functionData, ItemSetting itemSetting) : base(config)
        {
            _functionData = functionData ?? throw new Exception($"{nameof(this.GetType)}: FunctionData is null.");
            cellData = functionData.cellData;
            ItemSetting = itemSetting;
        }
        public override void Cancel()
        {
            base.Cancel();
            FunctionData.TestResult = (TestStatus.CANCEL, "");
        }
        public override void Stop()
        {
            base.Stop();
            FunctionData.TestResult = (TestStatus.FAILED, "");
        }
        public override void Run()
        {
            FunctionData.TurnInit();
            if (RetryTimes == 0)
                Logger.AddLog("----------------------[Begin]----------------------");
            else
                Logger.AddLog($"---------------------[Retry-{RetryTimes}]---------------------");
            FunctionData.TestResult = Test();
        }
        protected int RetryTimes => FunctionData.RetryTimes;
        protected void SetErrorCode(string errorCode)
        {
            FunctionData.SetTempErrorCode(errorCode);
        }
        protected MyLogger Logger => _functionData.logger;
        protected MyProperties Properties => cellData.CellProperties;
        protected TestData TestData => cellData.TestData;
        public FunctionData FunctionData => _functionData;

        public ItemSetting ItemSetting { get; private set; }

        public BasefunctionConfig BaseConfig => Config;

        public bool IsAcceptable => !FunctionData.IsFailed || ItemSetting.IsSkipFailure;

        protected abstract (TestStatus status, string value) Test();

    }
}
