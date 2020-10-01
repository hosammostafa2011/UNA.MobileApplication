using Helper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UNA.MobileApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideoDetailPage : ContentPage
    {
        public VideoDetailPage(VIDEO vVIDEO)
        {
            InitializeComponent();
            BodyView.Html = string.Format(@"<iframe style='width:100%;height:100%;'
                    src='https://www.youtube.com/embed/{0}?autoplay=1'
                    frameborder='0' allow='accelerometer; autoplay; clipboard-write;
                    encrypted-media; gyroscope; picture-in-picture' allowfullscreen>
                    </iframe>", vVIDEO.Video_Code, Application.Current.MainPage.Width);
            lblTitle.Text = vVIDEO.Title;
        }
    }
}