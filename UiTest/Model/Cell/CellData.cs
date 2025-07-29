using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using UiTest.Common;
using UiTest.Config;
using UiTest.Model.Function;
using UiTest.Service.ErrorCode;
using UiTest.Service.Logger;

namespace UiTest.Model.Cell
{
    public class CellData
    {
        public readonly string Name;
        public readonly CellLogger CellLogger;
        public readonly MyProperties CellProperties;
        public readonly TestData TestData;
        public readonly ErrorCodeMapper errorCodeMapper;
        private bool hasEnd;
        private TestStatus _processStatus;
        private Brush standbyColor;
        private Brush testColor;
        private Brush passColor;
        private Brush cancelColor;
        private Brush failColor;
        private TestMode _testMode;
        private readonly List<string> messageLines;

        public CellData(string name)
        {
            Name = name = name.ToUpper();
            CellProperties = new MyProperties(name);
            TestData = new TestData(name);
            CellLogger = new CellLogger(TestData);
            errorCodeMapper = ErrorCodeMapper.Instance;
            messageLines = new List<string>();
            TestStatus = TestStatus.STANDBY;
        }
        public TestMode TestMode
        {
            get => _testMode;
            set
            {
                Reset();
                _testMode = value;
                StandbyColor = value?.StandbyColor;
                CancelColor = value?.CancelColor;
                PassColor = value?.PassColor;
                CellProperties.SetProperties(value?.Properties);
            }
        }
        public void AddFuntionData(FunctionData functionData)
        {
            TestData.AddFuntionData(functionData);
        }
        public void AddFailedFuntionData(FunctionData functionData)
        {
            TestData.AddFailedFuntionData(functionData);
        }

        public TestStatus TestStatus
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
        public bool HasFailedFunctions => TestData.FunctionFailedDatas.Count > 0;
        public Brush StandbyColor { get => standbyColor; internal set { standbyColor = value; DataChaned?.Invoke(this); } }
        public Brush TestColor { get => testColor; internal set { testColor = value; DataChaned?.Invoke(this); } }
        public Brush PassColor { get => passColor; internal set { passColor = value; DataChaned?.Invoke(this); } }
        public Brush CancelColor { get => cancelColor; internal set { cancelColor = value; DataChaned?.Invoke(this); } }
        public Brush FailColor { get => failColor; internal set { failColor = value; DataChaned?.Invoke(this); } }

        public string GetMessage()
        {
            switch (TestStatus)
            {
                case TestStatus.STANDBY:
                    return TestStatus.STANDBY.ToString();
                default:
                    StringBuilder messBuilder = new StringBuilder();
                    if (TestStatus == TestStatus.FAILED)
                    {
                        messBuilder.AppendLine($"{TestStatus}: \"{TestData.ErrorCode}\"");
                    }
                    else
                    {
                        messBuilder.AppendLine(TestStatus.ToString());
                    }
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
                TestData.Reset();
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
                TestData.Start(input, modeName);
                TestStatus = TestStatus.TESTING;
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
                TestData.End();
                CellLogger.CreateLog();
                CellLogger.SaveLog();
                TestStatus = TestData.Result;
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
                TestData.EndProcess();
                CellLogger.CreateLog();
                CellLogger.SaveLog();
                TestStatus = TestData.FinalResult;

            }
            finally
            {
                DataChaned?.Invoke(this);
            }
        }
        public void AddMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message)) return;
            messageLines.Add(message);
        }

        public void SetExecption(string exceptionMessage)
        {
            TestStatus = TestStatus.FAILED;
            AddMessage($"Exception: {exceptionMessage}");
        }
    }
}
