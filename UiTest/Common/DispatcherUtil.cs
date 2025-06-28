using System;
using System.Threading.Tasks;
using System.Windows;

namespace UiTest.Common
{
    internal static class DispatcherUtil
    {
        public static bool IsAccess => Application.Current?.Dispatcher?.CheckAccess() ?? false;

        public static void RunOnUI(Action action)
        {
            var dispatcher = Application.Current?.Dispatcher;
            if(dispatcher == null || dispatcher.CheckAccess())
            {
                action();
            }
            else
            {
                dispatcher.Invoke(action);
            }
        }

        public static T RunOnUI<T>(Func<T> action)
        {
            var dispatcher = Application.Current?.Dispatcher;
            if (dispatcher == null || dispatcher.CheckAccess())
            {
                return action();
            }
            else
            {
                return dispatcher.Invoke(action);
            }
        }

        public static void RunAsyncOnUI(Action action)
        {
            var dispatcher = Application.Current?.Dispatcher;
            if (dispatcher == null || dispatcher.CheckAccess())
            {
                action();
            }
            else
            {
                dispatcher.BeginInvoke(action);
            }
        }
    }
}
