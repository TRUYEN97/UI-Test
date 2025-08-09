
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UiTest.Config;
using UiTest.ModelView;
using UiTest.Service.CellService;
using UiTest.Service.Factory;
using UiTest.Service.Logger;

namespace UiTest.Service.Managements
{
    public class CellManagement
    {
        private readonly List<Cell> cellTests;
        private readonly ViewModelFactory viewFactory;
        public ObservableCollection<BaseSubModelView> Cells { get; private set; } = new ObservableCollection<BaseSubModelView>();
        public CellManagement()
        {
            cellTests = new List<Cell>();
            viewFactory = ViewModelFactory.Instance;
        }

        public Cell GetCell(int index)
        {
            if (cellTests.Count > index && index >= 0)
            {
                return cellTests[index];
            }
            return null;
        }
        public BaseSubModelView GetBaseSubModelView(int index)
        {
            if (Cells.Count > index && index >= 0)
            {
                return Cells[index];
            }
            return null;
        }
        public void Clear()
        {
            cellTests.Clear();
            Cells.Clear();
        }

        public bool AddCell(string typeName)
        {
            string name = $"Slot-{cellTests.Count}";
            var view = viewFactory.GetInstanceWithTypeName(typeName, name);
            var cell = new Cell(name, view, cellTests.Count);
            cellTests.Add(cell);
            Cells.Add(view);
            return true;
        }

        public void RemoveCell(int index)
        {
            if (cellTests.Count > index && index >= 0)
            {
                cellTests.RemoveAt(index);
                Cells.RemoveAt(index);
            }
        }

        public void UpdataMode(TestMode selectedMode)
        {
            foreach (var cell in cellTests)
            {
                cell?.UpdateMode(selectedMode);
            }
        }

        public int Count => cellTests.Count;

        public bool IsAllFree => cellTests.All(cell => cell.IsFree);

        public bool TryGetCell(int index, out Cell cell)
        {
            return (cell = GetCell(index)) != null;
        }

        public void StartTest(string input, int index)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return;
            }
            if (TryGetCell(index, out Cell cell))
            {
                if (cell.IsFree)
                {
                    input = input.ToUpper().Trim();
                    foreach (var c in cellTests)
                    {
                        if (!c.IsFree && c.Input == input)
                        {
                            ProgramLogger.AddError("Core", $"Input: [{input}] is running at [{c.Name}].");
                            return;
                        }
                    }
                    cell.StartTest(input);
                }
                else
                {
                    ProgramLogger.AddError("Core", $"[{cell.Name}] is running.");
                }
            }
            else
            {
                ProgramLogger.AddError("Core", $"Index: {index} not exists.");
            }
        }
    }
}
