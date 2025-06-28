
using System.Diagnostics;
using System.Reflection;

namespace UiStore.Common
{
    internal class ProgramInfo
    {
        internal static string ProductVersion => FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
        internal static string ProductName => FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductName;
        internal static string CompanyName => FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).CompanyName;
    }
}
