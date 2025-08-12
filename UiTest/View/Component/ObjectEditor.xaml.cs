using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using UiTest.Converter;

namespace UiTest.View.Component
{
    public partial class ObjectEditor : UserControl
    {
        public static readonly DependencyProperty TargetObjectProperty =
            DependencyProperty.Register(nameof(TargetObject), typeof(object), typeof(ObjectEditor),
                new PropertyMetadata(null, OnTargetObjectChanged));

        public object TargetObject
        {
            get => GetValue(TargetObjectProperty);
            set => SetValue(TargetObjectProperty, value);
        }

        public ObjectEditor()
        {
            InitializeComponent();
        }

        private static void OnTargetObjectChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ObjectEditor editor)
                editor.BuildEditor();
        }

        private void BuildEditor()
        {
            EditorPanel.Children.Clear();
            if (TargetObject == null) return;

            var props = TargetObject.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead);

            foreach (var prop in props)
            {
                var stack = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 0, 0, 5) };

                stack.Children.Add(new TextBlock
                {
                    Text = prop.Name,
                    Width = 100,
                    VerticalAlignment = VerticalAlignment.Center
                });
                FrameworkElement editorControl = prop.CanWrite ? CreateEditorControl(prop) : CreateReadOnlyControl(prop);
                if (editorControl != null)
                {
                    stack.Children.Add(editorControl);
                }
                EditorPanel.Children.Add(stack);
            }
        }

        private FrameworkElement CreateEditorControl(PropertyInfo prop)
        {
            var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
            if (type == typeof(string))
            {
                var tb = new TextBox { Width = 200 };
                tb.SetBinding(TextBox.TextProperty, CreateBinding(prop.Name));
                return tb;
            }
            if (type == typeof(bool))
            {
                var cb = new CheckBox { VerticalAlignment = VerticalAlignment.Center };
                cb.SetBinding(CheckBox.IsCheckedProperty, CreateBinding(prop.Name));
                return cb;
            }
            if (type.IsEnum)
            {
                var combo = new ComboBox { Width = 200, ItemsSource = Enum.GetValues(type) };
                combo.SetBinding(ComboBox.SelectedItemProperty, CreateBinding(prop.Name));
                return combo;
            }
            if (type == typeof(DateTime))
            {
                var dp = new DatePicker { Width = 200 };
                dp.SetBinding(DatePicker.SelectedDateProperty, CreateBinding(prop.Name));
                return dp;
            }
            if (type.IsPrimitive || type == typeof(decimal))
            {
                var tb = new TextBox { Width = 200 };
                tb.SetBinding(TextBox.TextProperty, CreateBinding(prop.Name, true));
                return tb;
            }

            return new TextBlock
            {
                Text = prop.GetValue(TargetObject)?.ToString() ?? "",
                Width = 200
            };
        }
        private FrameworkElement CreateReadOnlyControl(PropertyInfo prop)
        {
            var value = prop.GetValue(TargetObject)?.ToString() ?? "";
            return new TextBox
            {
                Width = 200,
                IsReadOnly = true,
                Text = value
            };
        }

        private System.Windows.Data.Binding CreateBinding(string propertyName, bool convertNumber = false)
        {
            var binding = new System.Windows.Data.Binding(propertyName)
            {
                Source = TargetObject,
                Mode = System.Windows.Data.BindingMode.TwoWay,
                UpdateSourceTrigger = System.Windows.Data.UpdateSourceTrigger.PropertyChanged
            };
            if (convertNumber)
            {
                binding.ValidatesOnExceptions = true;
                binding.NotifyOnValidationError = true;
                binding.Converter = new NumberConverter();
            }
            return binding;
        }
    }
}

