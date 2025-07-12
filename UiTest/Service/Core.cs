using System;
using UiTest.Config;
using UiTest.Service.Interface;
using UiTest.Service.Managements;

namespace UiTest.Service
{
    internal class Core:IUpdate
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
    }
}
