using System.Text.RegularExpressions;

namespace Helper
{
    public class Constant
    {
        public static string ApiUrl = "http://api.una-oic.org/UNA/";
        public static string ApiHostName
        {
            get
            {
                var apiHostName = Regex.Replace(ApiUrl, @"^(?:http(?:s)?://)?(?:www(?:[0-9]+)?\.)?", string.Empty, RegexOptions.IgnoreCase)
                                   .Replace("/", string.Empty);
                return apiHostName;
            }
        }
    }
}