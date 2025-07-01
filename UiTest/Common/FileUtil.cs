using System.IO;
using System.Runtime.Serialization;

namespace UiTest.Common
{
    internal static class FileUtil
    {
        public static void WriteAllText(string filePath, string text, bool append = false)
        {
            string dir = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            if (append) 
            {
                File.AppendAllText(filePath, text);
            }
            else
            {
                File.WriteAllText(filePath, text);
            }
        }
        public static void WriteAllText(string filePath, ISerializable text, bool append = false)
        {
            string dir = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            if (append) 
            {
                File.AppendAllText(filePath, text?.ToString());
            }
            else
            {
                File.WriteAllText(filePath, text?.ToString());
            }
        }

        public static string ReadAllText(string filePath) 
        { 
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                return null;
            }
            return File.ReadAllText(filePath);
        }

        public static string[] ReadAllLines(string filePath) 
        { 
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                return null;
            }
            return File.ReadAllLines(filePath);
        }

        public static byte[] ReadAllByte(string filePath) 
        { 
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                return null;
            }
            return File.ReadAllBytes(filePath);
        }
    }
}
