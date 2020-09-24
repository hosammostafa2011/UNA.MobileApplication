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
    public partial class PhotoAlbum : ContentPage
    {
        PhotoAlbumViewModel photoAlbumViewModel = null;
        public PhotoAlbum()
        {
            InitializeComponent();
            BindingContext = photoAlbumViewModel = new PhotoAlbumViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = 0;
            Padding = safeInsets;
            if (photoAlbumViewModel.obsCollectionPHOTO_ALBUM.Count == 0)
                photoAlbumViewModel.LoadPHOTO_ALBUMCommand.Execute(null);
        }
    }
}