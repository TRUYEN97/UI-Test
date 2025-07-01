using System;
using System.Windows;
using System.Windows.Input;
using UiTest.Behaviors;

namespace UiTest.Common
{
    internal static class DependencyUtil
    {
        public static DependencyProperty RegisterDependencyProperty<T>(string key, Type ownerType, T defaultData)
        {
            return DependencyProperty.Register(key, typeof(T), ownerType, new PropertyMetadata(defaultData));
        }
        
        public static DependencyProperty RegisterDependencyProperty<T>(string key, Type ownerType)
        {
            return DependencyProperty.Register(key, typeof(T), ownerType, new PropertyMetadata(null));
        }
        public static DependencyProperty RegisterAttachedDependencyProperty<T>(string key, Type ownerType, PropertyMetadata propertyMetadata)
        {
            return DependencyProperty.RegisterAttached( key, typeof(T), ownerType, propertyMetadata);
        }
    }
}
