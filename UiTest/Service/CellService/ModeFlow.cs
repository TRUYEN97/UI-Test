using System;
using System.Collections.Generic;
using System.Windows.Media;
using UiTest.Config;

namespace UiTest.Service.CellService
{
    public class ModeFlow
    {
        public ModeFlow() 
        {
        }
        public int Loop => 1;
        public Brush TestColor { get; private set; }
        public Brush FailColor { get; private set; }
        public bool IsCoreGroup { get; private set; }

        public void Reset()
        {

        }

        public List<ItemConfig> GetListItem()
        {
            return null;
        }

        public void NextToPassFlow()
        {
            
        }

        public void NextToFailedFlow()
        {
            
        }
    }
}
