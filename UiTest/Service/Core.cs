using System;
using System.Reflection;
using System.Windows;
using UiTest.Config;
using UiTest.Service.CellService;
using UiTest.Service.Interface;
using UiTest.Service.Logger;
using UiTest.Service.Managements;

namespace UiTest.Service
{
    public class Core : IUpdate
    {
        private static readonly Lazy<Core> _instace = new Lazy<Core>(() => new Core());
        public static Core Instance = _instace.Value;
        private Core()
        {
            ProgramConfig = ConfigLoader.ProgramConfig;
            CellManagement = new CellManagement();
            ModelManagement = new ModelManagement(ProgramConfig, CellManagement);
            ViewBuilder = new ViewBuilder(ProgramConfig.ProgramSetting, CellManagement);
        }
        public ProgramConfig ProgramConfig { get; private set; }
        public ModelManagement ModelManagement { get; private set; }
        public CellManagement CellManagement { get; private set; }
        public ViewBuilder ViewBuilder { get; private set; }

        public bool Update()
        {
            try
            {
                bool rs = true;
                if (!this.ViewBuilder.Update())
                {
                    rs = false;
                }
                if (!ModelManagement.Update())
                {
                    rs = false;
                }
                return rs;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Start(string input)
        {
            StartTest(input, 0);
        }
        public void Start(string input, string index)
        {
            if (int.TryParse(index, out int id))
            {
                StartTest(input, id);
            }
            else
            {
                ProgramLogger.AddError("Core", $"Index invalid: {index}");
            }
        }
        private void StartTest(string input, int index)
        {
            if (this.CellManagement.TryGetCell(index, out Cell cell))
            {
                cell.StartTest(input);
            }
            else
            {
                ProgramLogger.AddError("Core", $"Index: {index} not exists!");
            }
        }
    }
}
