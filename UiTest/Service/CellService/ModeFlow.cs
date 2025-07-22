using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using UiTest.Common;
using UiTest.Config;

namespace UiTest.Service.CellService
{
    public class ModeFlow
    {
        private readonly ModeConfig modeConfig;
        private readonly ProgramConfig _programConfig;
        private ItemGroup _itemGroup;

        public ModeFlow(ModeConfig groupName, ProgramConfig programConfig)
        {
            modeConfig = groupName;
            _programConfig = programConfig;
        }

        public int Loop => modeConfig.LoopTimes < 1 ? 1 : modeConfig.LoopTimes;
        public Brush TestColor => Util.GetBrushFromString(_itemGroup?.TestColor, Brushes.Gold);
        public Brush FailColor => Util.GetBrushFromString(_itemGroup?.FailColor, Brushes.Brown);
        public bool IsFinalGroup => _itemGroup.IsFinalGroup;

        public bool Reset()
        {
            return SetItemGroup(modeConfig.GroupName);
        }

        public List<ItemConfig> GetListItem()
        {
            return _itemGroup.Items.Where(
                (i) => _programConfig.ItemConfigs.ContainsKey(i))
                .Select(i => _programConfig.ItemConfigs[i]).ToList();
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
            return _programConfig?.ItemGroups?.TryGetValue(groupName, out _itemGroup) == true;
        }
    }
}
