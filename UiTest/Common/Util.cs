using System;
using System.Windows;

namespace UiTest.Common
{
    internal static class Util
    {
        public static DependencyProperty CreateDependencyProperty<T>(string key, Type ownerType, T defaultData)
        {
            return DependencyProperty.Register(key, typeof(T), ownerType, new PropertyMetadata(defaultData));
        }
        
        public static DependencyProperty CreateDependencyProperty<T>(string key, Type ownerType)
        {
            return DependencyProperty.Register(key, typeof(T), ownerType, new PropertyMetadata(null));
        }
    }
}
