using Plugin.SecureStorage;
using System;
using UNA.MobileApplication.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace UNA.MobileApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NationPage : ContentPage
    {
        private NationViewModel _NationViewModel = null;

        public NationPage(string categoryID, string categoryName, string nationID)
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

            string strLanguage = string.Empty;
            if (CrossSecureStorage.Current.HasKey("Language"))
                strLanguage = CrossSecureStorage.Current.GetValue("Language");
            else
                strLanguage = "1";
            switch (strLanguage)
            {
                case "1":
                    Title = "الدول الأعضاء";
                    txtSearch.Placeholder = "البحث";
                    break;

                case "2":
                    Title = "Nations";
                    txtSearch.Placeholder = "Search";
                    break;

                case "3":
                    Title = "Nations";
                    txtSearch.Placeholder = "chercher";

                    break;
            }
            BindingContext = _NationViewModel = new NationViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = 0;
            Padding = safeInsets;
            if (_NationViewModel.obsCollectionNATION.Count == 0)
                _NationViewModel.LoadNATIONCommand.Execute(null);
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as NATION;
            if (item == null)
                return;
            ItemsListView.SelectedItem = null;
            await Navigation.PushAsync(new NewsList(string.Empty, item.Nation_Name, item.Nation_id));//for shared
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            //string x = e.NewTextValue;
            _NationViewModel.DoSearch(e.NewTextValue);
            //searchResults.ItemsSource = DataService.GetSearchResults(e.NewTextValue);
        }
    }
}