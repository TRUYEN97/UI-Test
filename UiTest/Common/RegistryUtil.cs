using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace UiTest.Common
{
    public class RegistryUtil
    {
        private readonly string SubKey;
        public RegistryUtil(string key)
        {
            SubKey = key;
        }
        public bool SaveValue<T>(string keyWord, T value)
        {
            if (string.IsNullOrWhiteSpace(SubKey)) { return false; }
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(SubKey))
            {
                if (value == null)
                {
                    key.SetValue(keyWord, "");
                }
                else
                {
                    key.SetValue(keyWord, value);
                }
                return true;
            }
        }

        public bool SaveIntValue(string keyWord, int value)
        {
            return SaveValue(keyWord, value);
        }

        public bool SaveLongValue(string keyWord, long value)
        {
            return SaveValue(keyWord, value);
        }

        public bool SaveDoubleValue(string keyWord, double value)
        {
            return SaveValue(keyWord, value);
        }

        public bool SaveStringValue(string keyWord, string value)
        {
            return SaveValue(keyWord, value);
        }
        public T GetValue<T>(string keyWord, T def)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(SubKey))
            {
                if (key == null)
                {
                    return def;
                }
                return (T)key.GetValue(keyWord, def);
            }
        }
    }
}
