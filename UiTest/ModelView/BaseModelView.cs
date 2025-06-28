using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using UiTest.Common;

namespace UiTest.ModelView
{
    internal abstract class BaseViewModel : INotifyPropertyChanged
    {
        protected BaseViewModel() { }
        public event PropertyChangedEventHandler PropertyChanged;
        protected SafeDispatcherProperty<T> CreateSafeProperty<T>(string name, T initialValue = null) where T : class
        {
            return new SafeDispatcherProperty<T>(
                Application.Current.Dispatcher,
                propertyName: name,
                raisePropertyChanged: (prop) => OnPropertyChanged(prop),
                initialValue: initialValue
            );
        }
        protected static LinearGradientBrush CreateLinearGradientColor(ICollection<(string, double)> values)
        {
            return new LinearGradientBrush()
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(1, 0),
                GradientStops = new GradientStopCollection(values?.Select((v) => new GradientStop((Color)ColorConverter.ConvertFromString(v.Item1), v.Item2)))
            };
        }
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            if (Application.Current?.Dispatcher?.CheckAccess() == true)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            else
                Application.Current?.Dispatcher?.Invoke(() =>
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name))
                );
        }

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string name = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(name);
            return true;
        }

        protected T SafeGet<T>(Func<T> getter) => DispatcherUtil.IsAccess ? getter() : DispatcherUtil.RunOnUI(getter);

        protected void SafeSet(Action setter)
        {
            if (DispatcherUtil.IsAccess)
                setter();
            else
                DispatcherUtil.RunOnUI(setter);
        }
    }
}
