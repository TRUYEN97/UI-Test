using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiTest.Mode
{
    internal class ModelManagement
    {
        public ObservableCollection<BaseMode> Modes {  get; private set; }
        public BaseMode SelectedMode { get; set; }
        public ModelManagement() { 
            Modes = new ObservableCollection<BaseMode>();
        }
    }
}
