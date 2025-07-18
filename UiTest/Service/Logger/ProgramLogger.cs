using System;
using System.Collections.ObjectModel;

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
                MessageBox.Add(log);
                if (MessageBox.Count > 20)
                {
                    MessageBox.RemoveAt(0);
                }
            });
        }
        public static ProgramLogger Instance => _instance.Value;

        public static void AddLog(string message)
        {
            Instance.logger.AddLog(message);
        }
        public static void AddError(string message)
        {
            Instance.logger.AddErrorText(message);
        }
        public static void AddWarning(string message)
        {
            Instance.logger.AddWarningText(message);
        }
        public static void AddInfo(string message)
        {
            Instance.logger.AddInfoText(message);
        }

    }
}
