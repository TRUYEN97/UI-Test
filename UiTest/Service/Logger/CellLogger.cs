
using System;
using System.IO;
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

        private void CreateHeaderLog()
        {
            myLogger.AddText($"===============================================================================================");
            myLogger.AddText($"-------------------------------------------[Test summary]-------------------------------------------");
            myLogger.AddText($"Product: {testData.Product}");
            myLogger.AddText($"Station: {testData.Station}");
            myLogger.AddText($"Pc name: {testData.PcName}");
            myLogger.AddText($"Cell name: {testData.CellName}");
            myLogger.AddText($"Start time: {testData.StartTime}");
            myLogger.AddText($"Stop time: {testData.StopTime}");
            myLogger.AddText($"Result: {testData.Result}");
            myLogger.AddText($"Error code: {testData.ErrorCode}");
            myLogger.AddText($"Cycle time: {testData.CycleTime}");
            if (testData.IsTested)
            {
                myLogger.AddText($"-------------------------------------------[Final summary]------------------------------------------");
                myLogger.AddText($"Final stop time: {testData.FinalStopTime}");
                myLogger.AddText($"Final result: {testData.FinalResult}");
                myLogger.AddText($"Final error code: {testData.FinalErrorCode}");
                myLogger.AddText($"Final Cycle time: {testData.FinalCycleTime}");
                myLogger.AddText($"----------------------------------------------------------------------------------------------------");
            }
            myLogger.AddText($"Items failed: {testData.FunctionFailedDatas}");
            myLogger.AddText($"===============================================================================================");
        }

        public string LogText => myLogger.LogText;

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
            string dir = Path.Combine(setting.Local_log, DateTime.Now.ToString("dd-MM-yyyy"), testData.CellName, testData.Result);
            string logPath = Path.Combine(dir, fileName);
            myLogger.SaveToFile(logPath);
        }

        private string CreateFileName()
        {
            var info = ConfigLoader.ProgramConfig.ProgramInfo;
            if (testData.IsTested)
            {
                return $"{testData.Result}_{testData.MAC}_{info.Product}_{info.Station}_{PcInfo.PcName}_{DateTime.Now:yyyyMMdd_HHmmss}.log";
            }
            else
            {
                return $"{testData.Result}_{testData.MAC}_{info.Product}_{info.Station}_{PcInfo.PcName}_{DateTime.Now:yyyyMMdd_HHmmss}_{testData.ErrorCode}.log";
            }
        }
    }
}
