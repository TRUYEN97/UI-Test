using System;
using System.Collections.Generic;
using System.Text;
using UiTest.Common;

namespace UiTest.Service.Logger
{
    public class UnitLogger
    {
        public readonly string Name;
        private readonly StringBuilder logBuilder;
        public readonly List<Action<string>> WriteLogCallBacks;
        public readonly List<Action> clearLogCallBacks;
        public UnitLogger(string name) {
            this.Name = name;
            logBuilder = new StringBuilder();
            WriteLogCallBacks = new List<Action<string>>();
            clearLogCallBacks = new List<Action>();
        }

        public void SaveToFile(string filePath)
        {
            FileUtil.WriteAllText(filePath, logBuilder);
        }

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
            clearLogCallBacks.Add(action);
        }

        public void AddLog(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }
            foreach (var line in message.Split('\n','\r'))
            {
                string log = $"{DateTime.Now:yyyy/MM/dd HH:mm:ss} -> {line}\r\n";
                logBuilder.Append(line);
                foreach (var action in WriteLogCallBacks)
                {
                    action.Invoke(line);
                }
            }
        }

        public void Clear()
        {
            foreach(var action in clearLogCallBacks)
            {
                action.Invoke();
            }
            logBuilder.Clear();
        }
    }
}
