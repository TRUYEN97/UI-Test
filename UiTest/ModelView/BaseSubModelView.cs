
using System;
using System.Collections.Generic;
using System.Windows.Media;
using UiTest.Common;
using UiTest.Config;
using UiTest.Service.CellService;
using UiTest.Service.Interface;

namespace UiTest.ModelView
{
    public abstract class BaseSubModelView : BaseViewModel, IUpdate
    {
        private readonly SafeDispatcherProperty<Brush> _color;
        private Cell _cell;
        private string _name;
        private string _testTime;
        private string _testStatus;
        private string _modeName;
        private TestMode _testMode;

        public BaseSubModelView() 
        {
            TestTime = "00.00:00:00";
            _color = CreateSafeProperty<Brush>(nameof(Color), new SolidColorBrush((Color)ColorConverter.ConvertFromString("#AA397C98")));
        }
        public Cell Cell
        {
            get => _cell;
            set
            {
                if (value == null) return;
                _cell = value;
                UpdateCellData(Cell);
            }
        }

        public TestMode TestMode 
        { 
            get => _testMode;
            set 
            {
                if (value == null) return;
                _testMode = value;
                Update();
            } 
        }

        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }
        public string TestTime { get => _testTime; set { _testTime = value; OnPropertyChanged(); } }
        public string TestStatus { get => _testStatus; set { _testStatus = value; OnPropertyChanged(); } }
        public string ModeName { get => _modeName; set { _modeName = value; OnPropertyChanged(); } }

        public Brush Color { get => _color.Value; set=> _color.Value = value; }

        public bool Update()
        {
            try
            {
                UpdateName();
                UpdateModeName();
                UpdateTimeTest();
                UpdateTestStatus();
                UpdateColor();
                UpdateCellData(Cell);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        protected virtual void UpdateTestStatus()
        {
            TestStatus = _cell.TestStatus;
            OnPropertyChanged(nameof(TestStatus));
        }

        protected virtual void UpdateTimeTest()
        {
            TestTime = _cell.StringTestTime;
            OnPropertyChanged(nameof(TestTime));
        }

        protected virtual void UpdateModeName()
        {
            ModeName = TestMode.Name;
            OnPropertyChanged(nameof(ModeName));
        }

        protected virtual void UpdateName()
        {
            Name = _cell.Name;
            OnPropertyChanged(nameof(Name));
        }
        protected virtual void UpdateColor()
        {
            Color = TestMode.StandbyColor;
            OnPropertyChanged(nameof(Color));
        }

        protected abstract void UpdateCellData(Cell cell);

    }
}
