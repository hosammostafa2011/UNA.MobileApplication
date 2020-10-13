using System.Text.RegularExpressions;

namespace Helper
{
    public class Constant
    {
        public static string ApiUrl = "https://api.una-oic.org";
        public static string NewsURL = "http://www.una-oic.org/page/public/news_details.aspx?id={0}";
        public static string PhotoAlbumURL = "http://www.una-oic.org/page/public/PhotoAlbum.aspx?ID={0}";
        public static string ApiHostName
        {
            get
            {
                var apiHostName = Regex.Replace(ApiUrl, @"^(?:http(?:s)?://)?(?:www(?:[0-9]+)?\.)?", string.Empty, RegexOptions.IgnoreCase)
                                   .Replace("/", string.Empty);
                return apiHostName;
            }
        }

        public static System.IFormatProvider VedioURL { get; set; }
    }
}