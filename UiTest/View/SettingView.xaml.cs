using System;
using System.Windows;
using UiTest.ModelView;

namespace UiTest.View
{
    /// <summary>
    /// Interaction logic for SettingView.xaml
    /// </summary>
    public partial class SettingView : Window
    {
        public SettingView(string configPath, string savePath)
        {
            InitializeComponent();
            DataContext = new SettingModelView(configPath, savePath);
        }
    }
}
