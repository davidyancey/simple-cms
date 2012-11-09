using System.Text.RegularExpressions;

namespace AgileDevDays.Core.Common
{
    public class QueryString
    {
        public static string Parse(string parameterName, string text)
        {
            Match expressionMatch = Regex.Match(text, string.Format(@"{0}=(?<value>[^&]+)", parameterName));

            if (!expressionMatch.Success)
            {
                return string.Empty;
            }

            return expressionMatch.Groups["value"].Value;
        }
    }
}