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
    public partial class NewsList : ContentPage
    {
        private readonly NewsListViewModel _newsListViewModel;
        public string CategoryId { get; set; }

        public NewsList(string categoryID)
        {
            InitializeComponent();
            CategoryId = categoryID;
            BindingContext = _newsListViewModel = new NewsListViewModel(CategoryId);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //var safeInsets = On<iOS>().SafeAreaInsets();
            //safeInsets.Bottom = 0;
            //Padding = safeInsets;
            //Shell.SetNavBarIsVisible(this, true);
            _newsListViewModel.LoadNewsCommand.Execute(CategoryId);
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