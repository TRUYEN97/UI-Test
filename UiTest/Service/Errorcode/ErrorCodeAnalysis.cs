using System;
using System.Text;
using UiTest.Common;

namespace UiTest.Service.ErrorCode
{
    internal class ErrorCodeAnalysis
    {

        private readonly ErrorCodeModel _errorCodeModel;
        public int MaxLength { get; set; }

        public ErrorCodeAnalysis(ErrorCodeModel errorCodeModel)
        {
            _errorCodeModel = errorCodeModel;
            MaxLength = Int16.MaxValue;
        }

        public bool TryGetErrorCode(string funcName, out string errorcode)
        {
            if (!_errorCodeModel.TryGet(funcName, out errorcode))
            {
            }
            return !string.IsNullOrWhiteSpace(errorcode);
        }
        public string CreateNewErrorcode(string functionName)
        {
            StringBuilder newErrorBuilder = new StringBuilder();
            if (string.IsNullOrEmpty(functionName))
            {
                for (int i = 0; i < MaxLength; i++)
                {
                    newErrorBuilder.Append('F');
                }
            }
            else
            {
                foreach (var c in functionName)
                {
                    if (newErrorBuilder.Length >= MaxLength)
                    {
                        break;
                    }
                    if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
                    {
                        newErrorBuilder.Append(c);
                    }
                }
            }
            return newErrorBuilder.ToString().ToUpper();
        }

        public string MakeUp(string errorCode)
        {
            if (string.IsNullOrWhiteSpace(errorCode))
            {
                return "";
            }
            if (errorCode.Length > MaxLength)
            {
                return errorCode.Substring(0, MaxLength).ToUpper().Trim();
            }
            return errorCode.ToUpper().Trim();
        }
    }
}
