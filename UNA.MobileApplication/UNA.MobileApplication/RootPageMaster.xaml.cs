using Helper;
using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UNA.MobileApplication.ViewModels;
using UNA.MobileApplication.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UNA.MobileApplication
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RootPageMaster : ContentPage
    {
        public ListView ListView;

        public RootPageMaster()
        {
            InitializeComponent();

            BindingContext = new RootPageMasterViewModel();
            ListView = MenuItemsListView;
            ListView.SeparatorVisibility = SeparatorVisibility.Default;
            ListView.SeparatorColor = Color.FromHex("#016b56");
        }

        private void Arabic_Tapped(object sender, EventArgs e)
        {
            CrossSecureStorage.Current.SetValue("Language", "1");
            HelperManger.ShowToast("تم تغيير لغة التطبيق إلى العربية");

            (Application.Current).MainPage = new RootPage();
        }

        private void English_Tapped(object sender, EventArgs e)
        {
            CrossSecureStorage.Current.SetValue("Language", "2");
            HelperManger.ShowToast("The language of the application has been changed to English");
            //myshell.FlowDirection = FlowDirection.LeftToRight;
            (Application.Current).MainPage = new RootPage();
        }

        private void French_Tapped(object sender, EventArgs e)
        {
            CrossSecureStorage.Current.SetValue("Language", "3");
            HelperManger.ShowToast("La langue de l’application a été modifiée pour Français");
            //myshell.FlowDirection = FlowDirection.LeftToRight;
            (Application.Current).MainPage = new RootPage();
        }
    }
}