using Helper;
using Helper.Model;
using Plugin.Share;
using Plugin.Share.Abstractions;
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
        public bool IsIOS { get; set; }
        public string lblVedio_ID { get; set; }
        public VideoDetailPage(VIDEO vVIDEO)
        {
            InitializeComponent();
            string _content = string.Format(@"<iframe style='width:100%;height:100%;'
                    src='https://www.youtube.com/embed/{0}?autoplay=1'
                    frameborder='0' allow='accelerometer; autoplay; clipboard-write;
                    encrypted-media; gyroscope; picture-in-picture' allowfullscreen>
                    </iframe>", vVIDEO.Video_Code);
            var html = new HtmlWebViewSource
            {
                Html = _content
            };
            BodyViewAndroid.Source = html;
            lblTitle.Text = vVIDEO.Title;
            lblVedio_ID = vVIDEO.Video_ID;
        }

        private void imgShare_Tapped(object sender, EventArgs e)
        {
            IShare shareInfo = CrossShare.Current;
            ShareMessage _ShareMessage = new ShareMessage();
            _ShareMessage.Text = lblTitle.Text;
            _ShareMessage.Url = string.Format(Constant.VedioURL, lblVedio_ID);
            shareInfo.Share(_ShareMessage);
        }
    }
}