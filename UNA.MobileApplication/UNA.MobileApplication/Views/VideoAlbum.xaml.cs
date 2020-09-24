﻿using System;
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
        VideoAlbumViewModel videoAlbumViewModel = null;
        public VideoAlbum()
        {
            InitializeComponent();
            BindingContext = videoAlbumViewModel = new VideoAlbumViewModel();
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
    }
}