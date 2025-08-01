﻿
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UiTest.Config;
using UiTest.ModelView;
using UiTest.Service.CellService;
using UiTest.Service.Factory;

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

        public void Clear()
        {
            cellTests.Clear();
            Cells.Clear();
        }

        public bool AddCell(string typeName)
        {
            string name = $"Slot-{cellTests.Count}";
            var view = viewFactory.GetInstanceWithTypeName(typeName, name);
            var cell = new Cell(name, view);
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
            if (selectedMode == null) return;
            foreach (var cell in cellTests)
            {
                if (cell?.IsFree == true)
                { 
                    cell.UpdateMode(selectedMode); 
                }
            }
        }

        public int Count => cellTests.Count;

        public bool TryGetCell(int index, out Cell cell)
        {
            return (cell = GetCell(index)) != null;
        }
    }
}
