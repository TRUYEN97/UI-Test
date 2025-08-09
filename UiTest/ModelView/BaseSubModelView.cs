
using System;
using System.Windows.Media;
using UiTest.Common;
using UiTest.Service.CellService;
using UiTest.Service.Interface;

namespace UiTest.ModelView
{
    public abstract class BaseSubModelView : BaseModelView, IUpdate
    {
        private readonly SafeDispatcherProperty<Brush> _color;
        private Cell _cell;
        private string _name;
        private string _testTime;
        private string _testStatus;
        private string _modeName;
        private string _message;

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

        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }
        public string TestTime { get => _testTime; set { _testTime = value; OnPropertyChanged(); } }
        public string Status { get => _testStatus; set { _testStatus = value; OnPropertyChanged(); } }
        public string ModeName { get => _modeName; set { _modeName = value; OnPropertyChanged(); } }
        public string Message { get => _message; set { _message = value; OnPropertyChanged(); } }
        public Brush Color { get => _color.Value; set { _color.Value = value; OnPropertyChanged(); } }
        public bool Update()
        {
            try
            {
                UpdateName();
                UpdateModeName();
                UpdateTimeTest();
                UpdateTestStatus();
                UpdateMessage();
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
            TestStatus testStatus = Cell.TestStatus;
            if (testStatus == TestStatus.PASSED)
            {
                Color = Cell.CellData.PassColor;
            }
            else if (testStatus == TestStatus.FAILED)
            {
                Color = Cell.CellData.FailColor;
            }
            else if (testStatus == TestStatus.CANCEL)
            {
                Color = Cell.CellData.CancelColor;
            }
            else if (testStatus == TestStatus.STANDBY)
            {
                Color = Cell.CellData.StandbyColor;
            }
            else if (testStatus == TestStatus.TESTING)
            {
                Color = Cell.CellData.TestColor;
            }
            Status = testStatus.ToString();
        }

        protected virtual void UpdateTimeTest()
        {
            TestTime = _cell.StringTestTime;
        }

        protected virtual void UpdateModeName()
        {
            ModeName = _cell.CellData.TestMode?.Name;
        }

        protected virtual void UpdateName()
        {
            Name = _cell.Name;
        }

        protected virtual void UpdateMessage()
        {
            Message = _cell.Message;
        }

        protected abstract void UpdateCellData(Cell cell);

    }
}
