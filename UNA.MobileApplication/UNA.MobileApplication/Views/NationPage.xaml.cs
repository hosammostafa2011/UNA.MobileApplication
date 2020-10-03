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
                    Title = "الدول الأعضاء";
                    break;

                case "3":
                    Title = "الدول الأعضاء";
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
    }
}