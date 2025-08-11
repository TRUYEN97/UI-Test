using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiTest.ModelView.TabItemViewModel
{
    public abstract class BaseTabSettingModelView : BaseModelView
    {
        protected BaseTabSettingModelView(string name)
        {
            Name = name;
        }
        public string Name { get; protected set; }
        public abstract void Reload();
        public abstract void Save();
    }
}
