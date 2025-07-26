using System;
using System.Collections.Generic;
using System.Text;
using UiTest.Common;

namespace UiTest.Service.Logger
{
    public class MyLogger
    {
        private readonly StringBuilder logBuilder;
        public readonly List<Action<string>> WriteLogCallBacks;
        public readonly List<Action> ClearLogCallBacks;
        public MyLogger() {
            logBuilder = new StringBuilder();
            WriteLogCallBacks = new List<Action<string>>();
            ClearLogCallBacks = new List<Action>();
        }

        public void SaveToFile(string filePath)
        {
            FileUtil.WriteAllText(filePath, logBuilder.ToString());
        }

        public string LogText => logBuilder.ToString();

        public void AddWriteActionCallback(Action<string> action)
        {
            if (action == null)
            {
                return;
            }
            WriteLogCallBacks.Add(action);
        }

        public void AddClearCallback(Action action)
        {
            if (action == null)
            {
                return;
            }
            ClearLogCallBacks.Add(action);
        }

        public void AddLog(string message)
        {
            AddLog(null, message);
        }

        public void AddLog(string key, string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return;
            }
            foreach (var line in text.Split('\n'))
            {
                string log;
                if (string.IsNullOrEmpty(key))
                {
                    log = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.sss}  {line?.Trim()}";
                }
                else
                {
                    log = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.sss}  [{key?.ToUpper().Trim()}] => {line?.Trim()}";
                }
                logBuilder.Append($"{log}\r\n");
                foreach (var action in WriteLogCallBacks)
                {
                    action.Invoke(log);
                }
            }
        }

        public void AddText(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return;
            }
            foreach (var line in text.Split('\n'))
            {
                string log = $"{line?.Trim()}";
                logBuilder.Append($"{log}\r\n");
                foreach (var action in WriteLogCallBacks)
                {
                    action.Invoke(log);
                }
            }
        }

        public void AddErrorText(string mess)
        {
            AddLog("Error", mess);
        }
        public void AddWarningText(string mess)
        {
            AddLog("Warning", mess);
        }
        public void AddInfoText(string mess)
        {
            AddLog("Info", mess);
        }

        public void Clear()
        {
            foreach(var action in ClearLogCallBacks)
            {
                action.Invoke();
            }
            logBuilder.Clear();
        }
    }
}
