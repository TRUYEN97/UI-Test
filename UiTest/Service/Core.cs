using System;
using System.Collections.ObjectModel;
using UiTest.Config;
using UiTest.ModelView.ListBoxItems;
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
            ActionTools = new ObservableCollection<ActionToolModelView>();
            ProgramConfig.ActionEvents.ActionTools.ForEach(i => { if (!string.IsNullOrWhiteSpace(i?.Name)) ActionTools.Add(new ActionToolModelView(i)); });
        }
        public ProgramConfig ProgramConfig { get; private set; }
        public ModelManagement ModelManagement { get; private set; }
        public CellManagement CellManagement { get; private set; }
        public ViewBuilder ViewBuilder { get; private set; }
        public ObservableCollection<ActionToolModelView> ActionTools { get; private set; }

        public bool Update()
        {
            try
            {
                bool rs = true;
                if (!ViewBuilder.Update())
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
            CellManagement.StartTest(input, index);
        }
    }
}
