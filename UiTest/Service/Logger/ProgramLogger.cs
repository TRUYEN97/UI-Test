using System;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using UiTest.Common;
using UiTest.Model.Cell;

namespace UiTest.Service.Logger
{
    public class ProgramLogger
    {
        private static readonly Lazy<ProgramLogger> _instance = new Lazy<ProgramLogger>(() => new ProgramLogger());
        private readonly MyLogger logger;
        public readonly ObservableCollection<string> MessageBox;
        private ProgramLogger()
        {
            logger = new MyLogger();
            MessageBox = new ObservableCollection<string>();
            logger.WriteLogCallBacks.Add((log) =>
            {
                try
                {
                    DispatcherUtil.RunOnUI(() => {
                        MessageBox.Add(log);
                        if (MessageBox.Count > 20)
                        {
                            MessageBox.RemoveAt(0);
                        }
                    });
                }
                catch (Exception)
                {
                }
            });
        }
        public static ProgramLogger Instance => _instance.Value;

        public static void AddLog(string name, string message)
        {
            Instance.logger.AddLog($"---------{name}-----------");
            Instance.logger.AddLog(message);
            Instance.logger.AddLog($"--------------------------");
        }
        public static void AddError(string name, string message)
        {
            Instance.logger.AddLog($"---------{name}-----------");
            Instance.logger.AddErrorText(message);
            Instance.logger.AddLog($"--------------------------");
        }
        public static void AddWarning(string name, string message)
        {
            Instance.logger.AddLog($"---------{name}-----------");
            Instance.logger.AddWarningText(message);
            Instance.logger.AddLog($"--------------------------");
        }
        public static void AddInfo(string name, string message)
        {
            Instance.logger.AddLog($"---------{name}-----------");
            Instance.logger.AddInfoText(message);
            Instance.logger.AddLog($"--------------------------");
        }

    }
}
