using Plugin.SecureStorage;
using Plugin.SharedTransitions;
using System;
using UNA.MobileApplication.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace UNA.MobileApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePageList : ContentPage
    {
        private readonly HomePageViewModel _homePageViewModel;

        public HomePageList()
        {
            InitializeComponent();
            if (Device.RuntimePlatform == Device.iOS)
            {
                //ToolbarItems.Add(new ToolbarItem()
                //{
                //    IconImageSource = "logowhite.png",
                //    Order = ToolbarItemOrder.Primary,                    
                //    Priority = 1
                //});
                //ToolbarItems.Add(new ToolbarItem()
                //{
                //    IconImageSource = "blank.png",
                //    Order = ToolbarItemOrder.Primary,
                //    Priority = 2
                //});

                if (CrossSecureStorage.Current.GetValue("Language").Equals("1"))
                {
                    grdArabic.IsVisible = true;
                    grdEnglish.IsVisible = false;
                }
                else
                {
                    grdArabic.IsVisible = false;
                    grdEnglish.IsVisible = true;

                }


            }

            try
            {
                if (CrossSecureStorage.Current.GetValue("Language").Equals("1"))
                {
                    FlowDirection = FlowDirection.RightToLeft;
                    if (Device.RuntimePlatform == Device.iOS)
                        imgLogo.Margin = new Thickness(50, 0, 0, 0);
                    else
                        imgLogo.Margin = new Thickness(50, 4, 0, 0);
                }
                else
                {
                    FlowDirection = FlowDirection.LeftToRight;
                    if (Device.RuntimePlatform == Device.iOS)
                        imgLogo.Margin = new Thickness(-50, 0, 0, 0);
                    else
                        imgLogo.Margin = new Thickness(50, 4, 0, 0);
                }
            }
            catch (Exception)
            {
                FlowDirection = FlowDirection.RightToLeft;
                CrossSecureStorage.Current.SetValue("Language", "1");
                if (Device.RuntimePlatform == Device.iOS)
                    imgLogo.Margin = new Thickness(50, 0, 0, 0);
                else
                    imgLogo.Margin = new Thickness(50, 4, 0, 0);
            }

            BindingContext = _homePageViewModel = new HomePageViewModel();

            try
            {
                VersionTracking.Track();
                var currentVersion = VersionTracking.CurrentVersion;
                _homePageViewModel.VersionTracking(currentVersion);
            }
            catch (System.Exception)
            {
            }
        }

        public HomePageList(string categoryID, string categoryName, string nationID)
        {
            InitializeComponent();
            BindingContext = _homePageViewModel = new HomePageViewModel();

            try
            {
                if (CrossSecureStorage.Current.GetValue("Language").Equals("1"))
                {
                    FlowDirection = FlowDirection.RightToLeft;
                    if (Device.RuntimePlatform == Device.iOS)
                        imgLogo.Margin = new Thickness(50, 0, 0, 0);
                    else
                        imgLogo.Margin = new Thickness(50, 4, 0, 0);

                }
                else
                {
                    FlowDirection = FlowDirection.LeftToRight;
                    if (Device.RuntimePlatform == Device.iOS)
                        imgLogo.Margin = new Thickness(-50, 0, 0, 0);
                    else
                        imgLogo.Margin = new Thickness(50, 4, 0, 0);
                }
            }
            catch (Exception)
            {
                FlowDirection = FlowDirection.RightToLeft;
                CrossSecureStorage.Current.SetValue("Language", "1");
                if (Device.RuntimePlatform == Device.iOS)
                    imgLogo.Margin = new Thickness(50, 0, 0, 0);
                else
                    imgLogo.Margin = new Thickness(50, 4, 0, 0); 
            }

            try
            {
                VersionTracking.Track();
                var currentVersion = VersionTracking.CurrentVersion;
                _homePageViewModel.VersionTracking(currentVersion);
            }
            catch (System.Exception)
            {
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = 0;
            Padding = safeInsets;
            //if (_homePageViewModel.obsCollectionNews.Count <= 0)
            //    _homePageViewModel.LoadHomeNewsCommand.Execute(null);
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as NEWS;
            if (item == null)
                return;
            // We can set the SelectedGroup both in binding or using the static method
            // SharedTransitionShell.SetTransitionSelectedGroup(this, item.Id.ToString());

            _homePageViewModel.SelectedNews = item;
            await Navigation.PushAsync(new NewsDetails(new NewsDetailsViewModel(item)));//for shared

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }
    }
}