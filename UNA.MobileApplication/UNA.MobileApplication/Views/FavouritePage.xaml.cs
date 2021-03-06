﻿using Plugin.SecureStorage;
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
    public partial class FavouritePage : ContentPage
    {
        private readonly NewsListViewModel _newsListViewModel;
        public string CategoryId { get; set; }

        public FavouritePage(string categoryID, string categoryName, string nationID)
        {
            InitializeComponent();
            try
            {
                if (CrossSecureStorage.Current.GetValue("Language").Equals("1"))
                    FlowDirection = FlowDirection.RightToLeft;
                else
                    FlowDirection = FlowDirection.LeftToRight;
            }
            catch (Exception)
            {
                FlowDirection = FlowDirection.RightToLeft;
                CrossSecureStorage.Current.SetValue("Language", "1");
            }

            CategoryId = categoryID;
            string _title = string.Empty;
            string strLanguage = string.Empty;
            if (CrossSecureStorage.Current.HasKey("Language"))
                strLanguage = CrossSecureStorage.Current.GetValue("Language");
            else
                strLanguage = "1";
            switch (strLanguage)
            {
                case "1":
                    _title = "المفضلة";
                    break;

                case "2":
                    _title = "Favourite";
                    break;

                case "3":
                    _title = "Mes favoris";
                    break;
            }
            BindingContext = _newsListViewModel = new NewsListViewModel("7000", _title, string.Empty);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //var safeInsets = On<iOS>().SafeAreaInsets();
            //safeInsets.Bottom = 0;
            //Padding = safeInsets;
            //Shell.SetNavBarIsVisible(this, true);
            _newsListViewModel.LoadNewsCommand.Execute("7000");
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as NEWS;
            if (item == null)
                return;
            // We can set the SelectedGroup both in binding or using the static method
            // SharedTransitionShell.SetTransitionSelectedGroup(this, item.Id.ToString());

            _newsListViewModel.SelectedNews = item;
            await Navigation.PushAsync(new NewsDetails(new NewsDetailsViewModel(item)));//for shared

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        private void ItemsListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            MessagingCenter.Send<string, NEWS>("MyApp", "LoadNews", e.Item as NEWS);
        }
    }
}