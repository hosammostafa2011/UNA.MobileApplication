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
    public partial class NationPage : ContentPage
    {
        private NationViewModel _NationViewModel = null;
        public NationPage()
        {
            InitializeComponent();
            switch (CrossSecureStorage.Current.GetValue("Language"))
            {
                case "1":
                    Title = "الدول الأعضاء";
                    break;

                case "2":
                    Title = "Nations";
                    break;

                case "3":
                    Title = "Nations";
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
            // We can set the SelectedGroup both in binding or using the static method
            // SharedTransitionShell.SetTransitionSelectedGroup(this, item.Id.ToString());

            // Manually deselect item.
            ItemsListView.SelectedItem = null;

            //photoAlbumViewModel.SelectedNews = item;
            await Navigation.PushAsync(new NewsList(string.Empty, item.Nation_Name, item.Nation_id));//for shared
        }
    }
}