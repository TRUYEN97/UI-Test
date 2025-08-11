using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UiTest.Service.ErrorCode;

namespace UiTest.ModelView.TabItemViewModel.SettingTabs
{
    public class TabErrorCodeSettingModelView : BaseTabSettingModelView
    {
        private readonly ErrorCodeMapperConfig errorCodeMapperConfig;
        private string localPath;
        private bool isShowMissing;
        private string newLocalPath;

        public string LocalPath { get => localPath; set { localPath = value; OnPropertyChanged(); } }
        public bool IsShowMissing { get => isShowMissing; set { isShowMissing = value; OnPropertyChanged(); } }
        public string NewLocalPath { get => newLocalPath; set { newLocalPath = value; OnPropertyChanged(); } }
        public TabErrorCodeSettingModelView(ErrorCodeMapperConfig errorCodeMapperConfig) : base("Error code")
        {
            this.errorCodeMapperConfig = errorCodeMapperConfig;
            Reload();
        }
        public override void Reload()
        {
            LocalPath = errorCodeMapperConfig?.LocalFilePath;
            NewLocalPath = errorCodeMapperConfig?.LocalNewFilePath;
            IsShowMissing = errorCodeMapperConfig?.ShowMissingErrorCode == true;
        }

        public override void Save()
        {
            if (errorCodeMapperConfig == null)
            {
                return;
            }
            errorCodeMapperConfig.LocalFilePath = LocalPath;
            errorCodeMapperConfig.LocalNewFilePath = NewLocalPath;
            errorCodeMapperConfig.ShowMissingErrorCode = IsShowMissing;
        }
    }
}
