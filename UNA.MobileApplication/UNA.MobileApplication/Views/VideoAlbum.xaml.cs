﻿using Helper.Model;
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
    public partial class VideoAlbum : ContentPage
    {
        private VideoAlbumViewModel videoAlbumViewModel = null;

        public VideoAlbum()
        {
            InitializeComponent();
            BindingContext = videoAlbumViewModel = new VideoAlbumViewModel();
            switch (CrossSecureStorage.Current.GetValue("Language"))
            {
                case "1":
                    Title = "المرئيات";
                    break;

                case "2":
                    Title = "Media";
                    break;

                case "3":
                    Title = "Médias";
                    break;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = 0;
            Padding = safeInsets;
            if (videoAlbumViewModel.obsCollectionVIDEO.Count == 0)
                videoAlbumViewModel.LoadVIDEOCommand.Execute(null);
        }

        [Obsolete]
        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as VIDEO;
            if (item == null)
                return;
            ItemsListView.SelectedItem = null;
            //open in browser
            //Device.OpenUri(new Uri(item.Video_Url));

            await Navigation.PushAsync(new VideoDetailPage(item));
        }
    }
}