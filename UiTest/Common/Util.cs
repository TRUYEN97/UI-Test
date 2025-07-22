using System.Text.RegularExpressions;
using System.Windows.Media;

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

        public static Brush GetBrushFromString(string hexColor, Brush brushDefault)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(hexColor))
                {
                    return brushDefault;
                }
                return new SolidColorBrush((Color)ColorConverter.ConvertFromString(hexColor));
            }
            catch (System.Exception)
            {
                return brushDefault;
            }
        }

    }
}
