
using System;
using UiTest.Config;
using UiTest.Service.Cell;
using UiTest.Service.Interface;

namespace UiTest.ModelView
{
    public abstract class BaseSubModelView : BaseViewModel, IUpdate
    {
        private CellTest _cell;
        private string _name;
        private string _testTime;
        private string _testStatus;
        private string _modeName;
        private TestMode _testMode;

        public BaseSubModelView() 
        {
            TestTime = "00.00:00:00";
        }
        public CellTest Cell
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
                ModeName = _testMode.Name;
            } 
        }

        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }
        public string TestTime { get => _testTime; set { _testTime = value; OnPropertyChanged(); } }
        public string TestStatus { get => _testStatus; set { _testStatus = value; OnPropertyChanged(); } }
        public string ModeName { get => _modeName; set { _modeName = value; OnPropertyChanged(); } }

        public bool Update()
        {
            try
            {
                UpdateName();
                UpdateModeName();
                UpdateTimeTest();
                UpdateTestStatus();
                UpdateView();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected virtual void UpdateTestStatus()
        {
            OnPropertyChanged(nameof(TestStatus));
        }

        protected virtual void UpdateTimeTest()
        {
            OnPropertyChanged(nameof(TestTime));
        }

        protected virtual void UpdateModeName()
        {
            OnPropertyChanged(nameof(ModeName));
        }

        protected virtual void UpdateName()
        {
            OnPropertyChanged(nameof(Name));
        }

        protected abstract bool UpdateView();

        protected abstract void UpdateCellData(CellTest cell);

    }
}
