
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
            myLogger.AddText($"-------------------------------------------[Test summary]-------------------------------------------");
            myLogger.AddText($"Product: {testData.Product}");
            myLogger.AddText($"Station: {testData.Station}");
            myLogger.AddText($"Pc name: {testData.PcName}");
            myLogger.AddText($"Cell name: {testData.CellName}");
            myLogger.AddText($"Input: {testData.INPUT}");
            myLogger.AddText($"MAC: {testData.MAC}");
            myLogger.AddText($"Start time: {testData.StartTime}");
            myLogger.AddText($"Stop time: {testData.StopTime}");
            myLogger.AddText($"Result: {testData.Result}");
            myLogger.AddText($"Error code: {testData.ErrorCode}");
            myLogger.AddText($"Cycle time: {testData.CycleTime}");
            if (!string.IsNullOrWhiteSpace(testData.FinalStopTime))
            {
                myLogger.AddText($"-------------------------------------------[Final summary]------------------------------------------");
                myLogger.AddText($"Final stop time: {testData.FinalStopTime}");
                myLogger.AddText($"Final result: {testData.FinalResult}");
                myLogger.AddText($"Final error code: {testData.ErrorCode}");
                myLogger.AddText($"Final Cycle time: {testData.FinalCycleTime}");
                myLogger.AddText($"----------------------------------------------------------------------------------------------------");
            }
            myLogger.AddText($"Items failed: {GetListFailedItems()}");
            myLogger.AddText($"===============================================================================================");
            myLogger.AddText($"===============================================================================================");
        }

        private string GetListFailedItems()
        {
            var items = testData.FunctionFailedDatas;
            if (items == null || items.Count == 0 ) 
            {
                return "[ ]";
            }
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in items)
            {
                stringBuilder.Append(item.ToString());
                stringBuilder.Append(',');
            }
            stringBuilder.Insert(0, '[');
            stringBuilder.Replace(',', ']', stringBuilder.Length - 1, 1);
            return stringBuilder.ToString();
        }

        private void CreateBodyLog()
        {
            foreach (var funcData in testData.FunctionDatas)
            {
                myLogger.AddText($"===============================================================================================");
                myLogger.AddText($"------------------------------[{funcData}]------------------------------");
                myLogger.AddText($"Value: {funcData.Value}");
                myLogger.AddText($"Result: {funcData.Result}");
                myLogger.AddText($"Test time: {funcData.TestTime} s");
                myLogger.AddText($"Error code: {funcData.ErrorCode}");
                myLogger.AddText($"Upper limit: {funcData.UpperLimit}");
                myLogger.AddText($"Lower limit: {funcData.LowerLimit}");
                myLogger.AddText($"Spec: {funcData.Spec}");
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
            var info = ConfigLoader.ProgramConfig.ProgramInfo;
            if (testData.Result == TestStatus.Passed)
            {
                return $"{testData.Result}_{testData.MAC}_{info.Product}_{info.Station}_{PcInfo.PcName}_{testData.StartDateTime:yyyy-MM-dd_HH-mm-ss}.log";
            }
            else
            {
                return $"{testData.Result}_{testData.MAC}_{info.Product}_{info.Station}_{PcInfo.PcName}_{testData.StartDateTime:yyyy-MM-dd_HH-mm-ss}_{testData.ErrorCode}.log";
            }
        }
    }
}
