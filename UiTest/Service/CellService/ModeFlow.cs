using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using UiTest.Common;
using UiTest.Config;
using UiTest.Config.Items;

namespace UiTest.Service.CellService
{
    public class ModeFlow
    {
        private readonly ModeConfig modeConfig;
        private readonly ProgramConfig _programConfig;
        public readonly string Name;
        private ItemGroup _itemGroup;

        public ModeFlow(ModeConfig groupName, ProgramConfig programConfig, string name)
        {
            modeConfig = groupName;
            _programConfig = programConfig;
            Name = name;
        }

        public int Loop => modeConfig.LoopTimes < 1 ? 1 : modeConfig.LoopTimes;
        public Brush FailColor => Util.GetBrushFromString(_itemGroup?.FailColor, Brushes.Red);
        public bool IsFinalGroup => _itemGroup.IsFinalGroup;
        public bool Reset()
        {
            return SetItemGroup(modeConfig.BeginGroup);
        }

        public List<FunctionConfig> GetListItem()
        {
            if (_itemGroup == null) return null;
            return _itemGroup.Items.Where(
                (i) => i.FunctionConfig != null && _programConfig.FunctionConfigs.ContainsKey(i.FunctionConfig))
                .Select(i =>
                {
                    var item = _programConfig.FunctionConfigs[i.FunctionConfig];
                    item.Name = i.Name;
                    item.ItemSetting = i;
                    return item;
                }).ToList();
        }

        public bool NextToPassFlow()
        {
            return SetItemGroup(_itemGroup?.NextToPassGroup);
        }

        public bool NextToFailedFlow()
        {
            return SetItemGroup(_itemGroup?.NextToFailGroup);
        }
        private bool SetItemGroup(string groupName)
        {
            if (string.IsNullOrWhiteSpace(groupName))
            {
                _itemGroup = null;
                return false;
            }
            return modeConfig?.ItemGroups?.TryGetValue(groupName, out _itemGroup) == true;
        }
    }
}
