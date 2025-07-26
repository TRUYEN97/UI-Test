using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UiTest.Service.Tester
{
    public class BaseTester
    {
        public BaseTester() { }
        public virtual bool IsRunning => currentTask != null && !currentTask.IsCompleted;

        protected Task currentTask;
    }
}
