using System;
using System.Collections.Generic;
using UiTest.Common;
using UiTest.Config;
using UiTest.Model.Function;
using UiTest.Service.Logger;

namespace UiTest.Model.Cell
{
    public class CellData
    {
        public readonly string Name;
        public readonly CellLogger CellLogger;
        public readonly CellProperties CellProperties;
        private readonly TestData testData;
        private bool hasEnd;
        public CellData(string name)
        {
            Name = name = name.ToUpper();
            CellProperties = new CellProperties(name);
            testData = new TestData(name);
            CellLogger = new CellLogger(testData);
            ProcessStatus = TestStatus.Standby.ToString();
        }
        public void AddFuntionData(FunctionData functionData)
        {
            testData.AddFuntionData(functionData);
        }

        public string ProcessStatus { get; private set; }
        public event Action<CellData> DataChaned;

        public void Reset()
        {
            try
            {
                hasEnd = false;
                CellLogger.Reset();
                testData.Reset();
                ProcessStatus = TestStatus.Standby.ToString();
            }
            finally
            {
                DataChaned?.Invoke(this);
            }
        }

        public void Start(string input, string modeName)
        {
            try
            {
                Reset();
                testData.Start(input, modeName);
                ProcessStatus = TestStatus.Testing.ToString();
            }
            finally
            {
                DataChaned?.Invoke(this);
            }
        }
        public void End()
        {
            try
            {
                testData.End();
                CellLogger.CreateLog();
                CellLogger.SaveLog();
                ProcessStatus = testData.Result.ToString();
                hasEnd = true;
            }
            finally
            {
                DataChaned?.Invoke(this);
            }
        }
        public void EndProcess()
        {
            try
            {
                if (!hasEnd)
                {
                    End();
                }
                testData.EndProcess();
                CellLogger.CreateLog();
                CellLogger.SaveLog();
                ProcessStatus = testData.FinalResult.ToString();
            }
            finally
            {
                DataChaned?.Invoke(this);
            }
        }
    }
}
