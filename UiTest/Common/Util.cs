using System.Text.RegularExpressions;

namespace UiTest.Common
{
    public class Util
    {
        public static string FindGroup(string str, string regex)
        {
            if (!string.IsNullOrEmpty(str) && !string.IsNullOrEmpty(regex))
            {
                Match match = Regex.Match(str, regex);
                if (match.Success)
                {
                    return match.Groups[1].Value;
                }
            }
            return null;
        }

    }
}
