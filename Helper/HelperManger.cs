using Acr.UserDialogs;
using Model.Mobile;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Helper
{
    public class HelperManger
    {
        public static IUserDialogs PageDialog = UserDialogs.Instance;

        public static ObservableCollection<RESPONSE> CastToResponse(string result)
        {
            var json = JsonConvert.DeserializeObject<List<RESPONSE>>(result);
            return new ObservableCollection<RESPONSE>(json);
        }

        public static String ConvertImageURLToBase64(String url)
        {
            WebClient myWebClient = new WebClient();
            Byte[] _byte = myWebClient.DownloadData(url);

            StringBuilder _sb = new StringBuilder();
            _sb.Append(Convert.ToBase64String(_byte, 0, _byte.Length));
            myWebClient.Dispose();
            return _sb.ToString();
        }

        public static List<String> CastToClass(string result, string tableName)
        {
            ObservableCollection<RESPONSE> rESPONSE = HelperManger.CastToResponse(result);
            if (!string.IsNullOrEmpty(rESPONSE[0].ERROR_MESSAGE))
            {
                return null;
            }
            else
            {
                var jsonString = JObject.Parse(rESPONSE[0].JSON);
                return jsonString.SelectToken(tableName).Select(jt => jt.ToObject<String>()).ToList();
            }
        }

        private static readonly string[] SizeSuffixes =
                    { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

        public static string SizeSuffix(Int64 value, int decimalPlaces = 1)
        {
            if (decimalPlaces < 0) { throw new ArgumentOutOfRangeException("decimalPlaces"); }
            if (value < 0) { return "-" + SizeSuffix(-value); }
            if (value == 0) { return string.Format("{0:n" + decimalPlaces + "} bytes", 0); }
            int mag = (int)Math.Log(value, 1024);
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));
            if (Math.Round(adjustedSize, decimalPlaces) >= 1000)
            {
                mag += 1;
                adjustedSize /= 1024;
            }
            return string.Format("{0:n" + decimalPlaces + "} {1}",
                adjustedSize,
                SizeSuffixes[mag]);
        }

        public static void ShowToast(string vMESSAGE)
        {
            ToastConfig tc = new ToastConfig(vMESSAGE);
            tc.SetPosition(ToastPosition.Top);
            tc.BackgroundColor = System.Drawing.Color.FromArgb(1, 133, 109);
            tc.MessageTextColor = System.Drawing.Color.FromArgb(221, 221, 221);
            PageDialog.Toast(tc);
        }

        //private static FlowDirection DEVICE_DIRECTION = Device.FlowDirection;

        /*public static TextAlignment GetTextAlignment(string pLANGUAGE)
        {
            if (DEVICE_DIRECTION.Equals(FlowDirection.RightToLeft)) // Arabic
            {
                if (pLANGUAGE.Equals("1"))
                {
                    return Device.RuntimePlatform == Device.iOS ? TextAlignment.Start : TextAlignment.End;
                }
                else
                {
                    return Device.RuntimePlatform == Device.iOS ? TextAlignment.End : TextAlignment.Start;
                }
            }
            else
            {
                if (pLANGUAGE.Equals("1"))
                {
                    return Device.RuntimePlatform == Device.iOS ? TextAlignment.End : TextAlignment.Start;
                }
                else
                {
                    return Device.RuntimePlatform == Device.iOS ? TextAlignment.Start : TextAlignment.End;
                }
            }
        }*/
    }
}