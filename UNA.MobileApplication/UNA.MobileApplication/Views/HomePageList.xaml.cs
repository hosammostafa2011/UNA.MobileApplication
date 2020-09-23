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
    public partial class HomePageList : ContentPage
    {
        private readonly HomePageViewModel _homePageViewModel;
        public HomePageList()
        {
            InitializeComponent();
            BindingContext = _homePageViewModel = new HomePageViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = 0;
            Padding = safeInsets;
            if (_homePageViewModel.obsCollectionNews.Count <= 0)
                _homePageViewModel.LoadHomeNewsCommand.Execute(null);
        }
    }
}