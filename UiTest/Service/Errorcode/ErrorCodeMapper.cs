using System;
using System.IO;
using System.Windows;
using UiTest.Config;
using UiTest.Service.Logger;

namespace UiTest.Service.ErrorCode
{
    public class ErrorCodeMapper
    {
        private readonly static Lazy<ErrorCodeMapper> instance = new Lazy<ErrorCodeMapper>(() => new ErrorCodeMapper());
        private readonly ErrorCodeModel model;
        private readonly ErrorCodeAnalysis errorCodeAnalysis;
        private readonly ErrorCodeMapperConfig _config;
        private ErrorCodeMapper()
        {
            _config = ConfigLoader.ProgramConfig.ErrorCode;
            model = new ErrorCodeModel() { MaxLength = _config.MaxLength };
            errorCodeAnalysis = new ErrorCodeAnalysis(model) { MaxLength = _config.MaxLength };
            LoadErrorCode();
        }
        public static ErrorCodeMapper Instance => instance.Value;
        public ErrorCodeMapperConfig Config => _config;
        public bool LoadErrorCode()
        {
            model.Clear();
            bool rs = AddErrorCode(_config.LocalFilePath);
            AddErrorCode(_config.LocalNewFilePath);
            return rs;
        }

        public bool TryGetErrorcode(string functionName, out string errorcode)
        {
            try
            {
                if (errorCodeAnalysis.TryGetErrorCode(functionName, out errorcode))
                {
                    return true;
                }
                errorcode = errorCodeAnalysis.CreateNewErrorcode(functionName);
                if (ConfigLoader.ProgramConfig.ProgramSetting.ShowMissingErrorCode)
                {
                    string mess = $"Missing of item[{functionName}]\r\nAuto create a new errorCode is [{errorcode}]";
                    ProgramLogger.AddWarning(ToString(), mess);
                    MessageBox.Show(mess);
                }
                if (!string.IsNullOrWhiteSpace(errorcode))
                {
                    UpdateNewErrorCode(functionName, errorcode);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                errorcode = null;
                return false;
            }
        }

        private void UpdateNewErrorCode(string functionName, string newErrorcode)
        {
            model.Set(functionName, newErrorcode);
            string line = $"{functionName};{newErrorcode}".ToUpper();
            UpdateToLocalFile(line);
        }

        private void UpdateToLocalFile(string line)
        {
            string filePath = _config.LocalNewFilePath;
            if (!File.Exists(filePath))
            {
                string dir = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
            }
            File.AppendAllText(filePath, line);
        }

        private bool AddErrorCode(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                return false;
            }
            string[] lines = File.ReadAllLines(filePath);
            if (lines == null) return false;
            foreach (var line in lines)
            {
                string[] elems = line.Split(new char[] { ';' });
                if (elems.Length >= 2)
                {
                    string funcName = elems[0];
                    string errorcode = elems[1];
                    model.Set(funcName, errorcode);
                }
            }
            return true;
        }

        public string MakeUp(string errorCode)
        {
            return errorCodeAnalysis.MakeUp(errorCode);
        }
    }
}
