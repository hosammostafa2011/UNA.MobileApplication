using Helper;
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