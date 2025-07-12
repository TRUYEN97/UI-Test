
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UiTest.Config;
using UiTest.ModelView;
using UiTest.Service.Cell;
using UiTest.Service.Factory;

namespace UiTest.Service.Managements
{
    public class CellManagement
    {
        private readonly Dictionary<string, CellTest> cellTests;
        private readonly ViewModelFactory viewFactory;
        public ObservableCollection<BaseSubModelView> Cells { get; private set; } = new ObservableCollection<BaseSubModelView>();
        public CellManagement()
        {
            cellTests = new Dictionary<string, CellTest>();
            viewFactory = ViewModelFactory.Instance;
        }

        public CellTest GetCell(string name)
        {
            if (cellTests.TryGetValue(name.ToUpper(), out CellTest cellTest)) { return cellTest; }
            return null;
        }

        public void Clear()
        {
            cellTests.Clear();
            Cells.Clear();
        }

        public bool AddCell(string typeName, string name, bool update = false)
        {
            if (string.IsNullOrWhiteSpace(name) || !viewFactory.Exists(typeName))
            {
                return false;
            }
            if (cellTests.ContainsKey(name) && !update)
            {
                return false;
            }
            var view = viewFactory.GetInstanceWithTypeName(typeName, name);
            var cell = new CellTest(name, view);
            cellTests[name] = cell;
            Cells.Add(view);
            return true;
        }

        public void RemoveCell(string name)
        {
            if (cellTests.TryGetValue(name, out var cell))
            {
                Cells.Remove(cell?.ViewModel);
            }
        }

        public void UpdataMode(TestMode selectedMode)
        {
            if(selectedMode == null) return;
            foreach (var cell in cellTests.Values)
            {
                if (cell == null || !cell.IsFree) continue;
                cell.UpdateMode(selectedMode);
            }
        }
    }
}
