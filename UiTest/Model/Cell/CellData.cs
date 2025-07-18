using System;
using System.Collections.Generic;
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
        public CellData(string name)
        {
            Name = name = name.ToUpper();
            CellProperties = new CellProperties(name);
            testData = new TestData(name);
            CellLogger = new CellLogger(testData);
            TestStatus = "Standby";
        }
        public void AddFuntionData(FunctionData functionData)
        {
            testData.AddFuntionData(functionData);
        }
        public List<FunctionData> FunctionDatas => testData.FunctionDatas;

        public string TestStatus { get; private set; }
        public event Action<CellData> DataChaned;

        public void Reset()
        {
            CellLogger.Reset();
            testData.Reset();
            TestStatus = "Standby";
            DataChaned?.Invoke(this);
        }

        public void Start(string input, string modeName)
        {
            testData.MAC = input.Trim().ToUpper();
            testData.Mode = modeName;
            TestStatus = "Testing";
            DataChaned?.Invoke(this);
        }

        public void End()
        {
            CellLogger.CreateLog();
            CellLogger.SaveLog();
            TestStatus = testData.FinalResult;
            DataChaned?.Invoke(this);
        }
    }
}
