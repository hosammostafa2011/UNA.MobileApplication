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