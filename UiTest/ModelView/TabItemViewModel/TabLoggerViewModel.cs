using System;
using System.Collections.ObjectModel;

namespace UiTest.ModelView.TabItemViewModel
{
    internal class TabLoggerViewModel : BaseTabItemViewModel
    {
        public ObservableCollection<string> Logs { get; private set; }

        public void AddLog(string log)
        {
            if (string.IsNullOrEmpty(log))
            {
                return;
            }
            foreach (var line in log.Split('\n', '\r'))
            {
                if (string.IsNullOrEmpty(line.Trim())) { continue; }
                Logs.Add(line);
            }
        }

        public void Clear()
        {
            Logs.Clear();
        }

        public TabLoggerViewModel()
        {
            Name = "Logger";
            Logs = new ObservableCollection<string>();
        }
    }
}
