using System;
using System.Collections.ObjectModel;
using UiTest.Config;
using UiTest.ModelView;
using UiTest.Service.Factory;
using UiTest.Service.Interface;
using UiTest.Service.Managements;

namespace UiTest.Service
{
    public class ViewBuilder:IUpdate
    {
        private readonly ProgramSetting programSetting;
        private readonly CellManagement cellManagement;
        public ViewBuilder(ProgramSetting programConfig, CellManagement cellManagement) {
            this.programSetting = programConfig;
            this.cellManagement = cellManagement;
        }

        public void Setting(int row, int column, string viewName)
        {
            Rows = row < 1 ? 1 : row;
            Columns = column < 1 ? 1 : column;
            ViewName = viewName;
        }

        public bool Update()
        {
            try
            {
                Setting(programSetting.Row, programSetting.Column, programSetting.View);
                cellManagement.Clear();
                if (programSetting.IsSingleView)
                {
                    cellManagement.AddCell(ViewName);
                }
                else
                {
                    for (int i = 0; i < Rows * Columns; i++)
                    {
                        cellManagement.AddCell(ViewName);
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public string ViewName { get; private set; }
        public int Rows { get; private set; } = 1;
        public int Columns { get; private set; } = 1;
        public ObservableCollection<BaseSubModelView> Cells => cellManagement.Cells;
    }
}
