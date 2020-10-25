using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

public class NEWS
{
    public string News_ID { get; set; }
    public string Publish_Date { get; set; }
    public string Thumbnail_Image_Path { get; set; }
    public string Main_Image_Path { get; set; }
    public string Title { get; set; }
    public string Brief { get; set; }
    public string Source_Tag { get; set; }
    public string Details { get; set; }
    public string RowNumber { get; set; }
    public string FavouriteImage { get; set; }
    public string FAVOURITE_IDS { get; set; }
    public string Category_ID { get; set; }
    public TextAlignment HorizontalDirection { get; set; }
    //private static string USER_LANGUAGE = CrossSecureStorage.Current.GetValue("Language");
    //private static FlowDirection DEVICE_DIRECTION = Device.FlowDirection;
    //public TextAlignment _hPos;

    //public TextAlignment HorizontalDirection
    //{
    //    get
    //    {
    //        if (DEVICE_DIRECTION.Equals(FlowDirection.RightToLeft)) // Arabic
    //        {
    //            if (USER_LANGUAGE.Equals("1"))
    //            {
    //                return TextAlignment.Start;
    //            }
    //            else
    //            {
    //                return TextAlignment.End;
    //            }
    //        }
    //        else
    //        {
    //            if (USER_LANGUAGE.Equals("1"))
    //            {
    //                return TextAlignment.End;
    //            }
    //            else
    //            {
    //                return TextAlignment.Start;
    //            }
    //        }
    //    }
    //}
}