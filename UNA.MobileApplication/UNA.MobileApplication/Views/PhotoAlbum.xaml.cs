using Plugin.SecureStorage;
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
        private PhotoAlbumViewModel photoAlbumViewModel = null;

        public PhotoAlbum()
        {
            InitializeComponent();
            string strLanguage = string.Empty;
            if (CrossSecureStorage.Current.HasKey("Language"))
                strLanguage = CrossSecureStorage.Current.GetValue("Language");
            else
                strLanguage = "1";
            switch (strLanguage)
            {
                case "1":
                    Title = "البوم الصور";
                    break;

                case "2":
                    Title = "Photo Album";
                    break;

                case "3":
                    Title = "Album Photo";
                    break;
            }
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

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as PHOTO_ALBUM;
            if (item == null)
                return;
            // We can set the SelectedGroup both in binding or using the static method
            // SharedTransitionShell.SetTransitionSelectedGroup(this, item.Id.ToString());

            // Manually deselect item.
            ItemsListView.SelectedItem = null;

            //photoAlbumViewModel.SelectedNews = item;
            await Navigation.PushAsync(new PhotoAlbumDetails(new PhotoAlbumDetailsViewModel(item)));//for shared
        }
    }
}