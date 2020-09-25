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
    }
}