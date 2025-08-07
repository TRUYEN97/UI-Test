using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UiTest.Functions.TestFunctions.Config;

namespace UiTest.Functions.TestFunctions
{
    public class FunctionCoverManagement : CoverManagement<BasefunctionConfig>
    {
        public void WaitForTaskDone()
        {
            var list = new List<BaseCover<BasefunctionConfig>>();
            while (covers.Count > 0)
            {
                list.Clear();
                list.AddRange(covers.Values);
                if (list.All(i => i is FunctionCover function && function.ItemSetting.IsDontWaitForMe))
                {
                    break;
                }
                Thread.Sleep(500);
            }
        }
    }
}
