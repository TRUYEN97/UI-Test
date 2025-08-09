
using System;
using System.IO;
using System.Text;
using UiTest.Common;
using UiTest.Config;
using UiTest.Model.Cell;

namespace UiTest.Service.Logger
{
    public class CellLogger
    {
        private readonly MyLogger myLogger;
        private readonly TestData testData;
        public CellLogger(TestData data)
        {
            myLogger = new MyLogger();
            testData = data;
        }

        public void Reset()
        {
            myLogger.Clear();
        }

        public void CreateLog()
        {
            Reset();
            CreateHeaderLog();
            CreateBodyLog();
        }

        public string LogText => myLogger.LogText;

        private void CreateHeaderLog()
        {
            myLogger.AddText($"-----------------------------------------------------------------------------------------------");
            myLogger.AddText($"----------------------------------------[Test summary]-----------------------------------------");
            myLogger.AddText($"Product: {testData.Product}");
            myLogger.AddText($"Station: {testData.Station}");
            myLogger.AddText($"Pc name: {testData.PcName}");
            myLogger.AddText($"Cell name: {testData.CellName}");
            myLogger.AddText($"Input: {testData.Input}");
            myLogger.AddText($"MAC: {testData.MAC}");
            myLogger.AddText($"Start time: {testData.StartTime}");
            myLogger.AddText($"Stop time: {testData.StopTime}");
            myLogger.AddText($"Result: {testData.Result}");
            myLogger.AddText($"Error code: {testData.ErrorCode}");
            myLogger.AddText($"Cycle time: {testData.CycleTime} s");
            if (!string.IsNullOrWhiteSpace(testData.FinalStopTime))
            {
                myLogger.AddText($"----------------------------------------[Final summary]----------------------------------------");
                myLogger.AddText($"Final stop time: {testData.FinalStopTime}");
                myLogger.AddText($"Final result: {testData.FinalResult}");
                myLogger.AddText($"Final error code: {testData.ErrorCode}");
                myLogger.AddText($"Final Cycle time: {testData.FinalCycleTime} s");
            }
            myLogger.AddText($"Items failed: {GetListFailedItems()}");
            myLogger.AddText($"-----------------------------------------------------------------------------------------------\r\n");
        }

        private string GetListFailedItems()
        {
            var items = testData.FunctionFailedDatas;
            if (items == null || items.Count == 0)
            {
                return "[ ]";
            }
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in items)
            {
                stringBuilder.Append(item.name);
                stringBuilder.Append(',');
            }
            stringBuilder.Insert(0, '[');
            stringBuilder.Replace(',', ']', stringBuilder.Length - 1, 1);
            return stringBuilder.ToString();
        }

        private void CreateBodyLog()
        {
            myLogger.AddText($"===============================================================================================");
            foreach (var funcData in testData.FunctionDatas)
            {
                var result = funcData.resultModel;
                myLogger.AddText($"--------------------------------------[{funcData}]-------------------------------------");
                myLogger.AddText($"Name: {result.Name}");
                myLogger.AddText($"Result: {result.Result}");
                myLogger.AddText($"Value: {result.Value}");
                myLogger.AddText($"Cycle time: {result.CycleTime} s");
                myLogger.AddText($"Start time: {result.StartTime}");
                myLogger.AddText($"Stop time: {result.StopTime}");
                myLogger.AddText($"Upper limit: {result.UpperLimit}");
                myLogger.AddText($"Lower limit: {result.LowerLimit}");
                myLogger.AddText($"Spec: {result.Spec}");
                myLogger.AddText($"Error code: {result.ErrorCode}");
                myLogger.AddText($"-----------------------------------------------------------------------------------------------");
                myLogger.AddText(funcData.logger.LogText);
                myLogger.AddText($"===============================================================================================");
            }
        }

        public void SaveLog()
        {
            var setting = ConfigLoader.ProgramConfig.ProgramSetting;
            string fileName = CreateFileName();
            string dir = Path.Combine(setting.Local_log, DateTime.Now.ToString("yyyy-MM-dd"), testData.CellName, testData.Result.ToString());
            string logPath = Path.Combine(dir, fileName);
            myLogger.SaveToFile(logPath);
        }

        private string CreateFileName()
        {
            var setting = ConfigLoader.ProgramConfig.ProgramSetting;
            string commonPartName = $"{testData.Result}_{testData.MAC}_{setting.Product}_{setting.Station}_{PcInfo.PcName}_{testData.StartDateTime:yyyy-MM-dd_HH-mm-ss}";
            if (testData.Result == TestResult.PASSED || testData.Result == TestResult.CANCEL)
            {
                return $"{commonPartName}.log".ToUpper();
            }
            else
            {
                return $"{commonPartName}_{testData.ErrorCode}.log".ToUpper();
            }
        }
    }
}
