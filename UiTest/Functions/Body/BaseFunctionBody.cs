﻿using System;
using System.Threading;
using UiTest.Common;
using UiTest.Functions.Config;
using UiTest.Functions.Interface;
using UiTest.Mapper;
using UiTest.Model.Cell;
using UiTest.Model.Function;
using UiTest.Service.Factory;
using UiTest.Service.Logger;

namespace UiTest.Functions.Body
{
    public abstract class BaseFunctionBody<T> : ITestFunction where T : BasefunctionConfig
    {
        protected int retryTimes;
        private readonly T _config;
        private readonly FunctionData _functionData;
        private readonly CellData cellData;
        protected BaseFunctionBody(FunctionData functionData) : this(functionData, null) { }
        protected BaseFunctionBody(FunctionData functionData, T config)
        {
            _functionData = functionData ?? throw new Exception($"{nameof(this.GetType)}: FunctionData is null.");
            cellData = functionData.cellData;
            _config = BaseFactory<T>.CreateInstance(typeof(T));
            if (config != null && !UpdateConfig(config))
            {
                throw new Exception($"{nameof(this.GetType)}: Update function Config failed. [{typeof(T)}]");
            }
        }

        protected void SetErrorCode(string errorCode)
        {
            FunctionData.SetTempErrorCode(errorCode);
        }

        public CancellationTokenSource Cts { get; set; }
        public bool UpdateConfig(T config)
        {
            if (_config == null) return false;
            if (MyObjectMapper.Copy(config, _config))
            {
                return true;
            }
            return false;
        }
        public bool IsCancellationRequested => Cts?.IsCancellationRequested == true;
        public void Cancel()
        {
            Cts?.Cancel();
        }
        public (TestStatus status, string value) Run(int times)
        {
            retryTimes = times;
            if (_config.IsCancelInDebugMode)
            {
                Logger.AddWarningText("Cancel running in debug mode!");
                return (TestStatus.CANCEL,"");
            }
            return Test();
        }
        protected MyLogger Logger => _functionData.logger;
        public FunctionData FunctionData => _functionData;
        public BasefunctionConfig BaseConfig => _config;
        protected MyProperties Properties => cellData.CellProperties;
        protected TestData TestData => cellData.TestData;
        public T Config => _config;
        protected abstract (TestStatus status, string value) Test();

    }
}
