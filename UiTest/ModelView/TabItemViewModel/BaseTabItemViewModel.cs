using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiTest.ModelView.TabItemViewModel
{
    public abstract class BaseTabItemViewModel: BaseViewModel
    {
        public string Name {  get; protected set; }
    }
}
