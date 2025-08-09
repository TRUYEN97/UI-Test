using System;
using System.Windows;
using UiTest.Common;
using UiTest.Functions.ActionEvents.Configs;

namespace UiTest.Functions.ActionEvents.Events
{
    public class LoginAction : BaseActionEvent<LoginConfig>
    {
        public LoginAction(LoginConfig config) : base(config)
        {
        }

        protected override TestResult Test()
        {
            return MessageBox.Show("Lg", "Login", MessageBoxButton.OKCancel) == MessageBoxResult.OK ? TestResult.PASSED : TestResult.FAILED;
        }
    }
}
