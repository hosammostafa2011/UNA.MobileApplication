using Helper;
using Plugin.SecureStorage;
using Plugin.Share;
using Plugin.Share.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNA.MobileApplication.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace UNA.MobileApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PhotoAlbumDetails : ContentPage
    {
        public string _sharePhoto = string.Empty;
        public string SharePhoto
        {
            get
            {
                try
                {
                    string strLanguage = string.Empty;
                    if (CrossSecureStorage.Current.HasKey("Language"))
                        strLanguage = CrossSecureStorage.Current.GetValue("Language");
                    else
                        strLanguage = "1";
                    switch (strLanguage)
                    {
                        case "1":
                            _sharePhoto = "شارك الصور";
                            break;

                        case "2":
                            _sharePhoto = "Share album";
                            break;

                        case "3":
                            _sharePhoto = "Partager des photos";
                            break;
                    }
                }
                catch (Exception)
                {
                }
                return _sharePhoto;
            }
        }
        public PhotoAlbumDetails(PhotoAlbumDetailsViewModel photoAlbumDetailsViewModel)
        {
            InitializeComponent();
            BindingContext = photoAlbumDetailsViewModel;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = 0;
            Padding = safeInsets;
            //if (photoAlbumDetailsViewModel.obsCollectionPHOTO_ALBUM.Count == 0)
            //photoAlbumDetailsViewModel.LoadPHOTO_ALBUMCommand.Execute(null);
            lblSharePhoto.Text = SharePhoto;
        }

        private void imgShare_Tapped(object sender, EventArgs e)
        {
            IShare shareInfo = CrossShare.Current;
            ShareMessage _ShareMessage = new ShareMessage();
            _ShareMessage.Text = lblAlbumTitle.Text;
            _ShareMessage.Url = string.Format(Constant.PhotoAlbumURL, lblAlbum_ID.Text);
            shareInfo.Share(_ShareMessage);
        }
    }
}