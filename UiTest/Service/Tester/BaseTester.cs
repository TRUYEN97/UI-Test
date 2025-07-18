using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiTest.Service.Tester
{
    public class BaseTester
    {
        private bool _stopTest;
        public BaseTester() { }
        public virtual bool IsRunning => currentTask != null && !currentTask.IsCompleted;
        protected bool StopTest 
        { 
            get => _stopTest;
            set 
            { 
                _stopTest = value;
            } 
        }

        protected Task currentTask;
    }
}
