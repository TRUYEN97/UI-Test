using System;
using System.Collections.Generic;
using System.Text;
using UiTest.Common;
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
        private TestStatus _processStatus;
        private readonly List<string> messageLines;

        public CellData(string name)
        {
            Name = name = name.ToUpper();
            CellProperties = new CellProperties(name);
            testData = new TestData(name);
            CellLogger = new CellLogger(testData);
            messageLines = new List<string>();
            ProcessStatus = TestStatus.STANDBY;
        }
        public void AddFuntionData(FunctionData functionData)
        {
            testData.AddFuntionData(functionData);
        }

        public TestStatus ProcessStatus
        {
            get => _processStatus;
            private set
            {
                if (_processStatus == TestStatus.FAILED) return;
                    _processStatus = value;
            }
        }

        public event Action<CellData> DataChaned;
        public string Message => GetMessage();

        public bool HasFailedFunctions => testData.FunctionFailedDatas.Count > 0;

        public string GetMessage()
        {
            switch (ProcessStatus)
            {
                case TestStatus.STANDBY:
                    return TestStatus.STANDBY.ToString();
                default:
                    StringBuilder messBuilder = new StringBuilder();
                    messBuilder.Append(ProcessStatus.ToString());
                    foreach (string line in messageLines) 
                    {
                        messBuilder.AppendLine();
                        messBuilder.Append(line);
                    }
                    return messBuilder.ToString();
            }
        }
        public void Reset()
        {
            try
            {
                hasEnd = false;
                CellLogger.Reset();
                testData.Reset();
                _processStatus = TestStatus.STANDBY;
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
                ProcessStatus = TestStatus.TESTING;
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
                ProcessStatus = testData.Result;
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
                ProcessStatus = testData.FinalResult;
                
            }
            finally
            {
                DataChaned?.Invoke(this);
            }
        }
        public void AddMessage(string message)
        {
            if(string.IsNullOrWhiteSpace(message)) return;
            messageLines.Add(message);  
        }

        public void SetExecption(string exceptionMessage)
        {
            ProcessStatus = TestStatus.FAILED;
            AddMessage($"Exception: {exceptionMessage}");
        }
    }
}
