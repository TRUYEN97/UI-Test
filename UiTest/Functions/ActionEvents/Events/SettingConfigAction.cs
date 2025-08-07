using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UiTest.Functions.ActionEvents.Configs;

namespace UiTest.Functions.ActionEvents.Events
{
    public class SettingConfigAction : BaseActionEvent<SettingConfig>
    {
        public SettingConfigAction(SettingConfig config) : base(config)
        {
        }

        protected override bool Test()
        {
            throw new NotImplementedException();
        }
    }
}
