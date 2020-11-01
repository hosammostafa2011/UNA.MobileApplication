using Helper;
using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNA.MobileApplication.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UNA.MobileApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingViewPage : ContentPage
    {
        private readonly SettingViewModel _settingViewModel;
        public SettingViewPage(string categoryID, string categoryName, string nationID)
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
                    Title = "الإعدادات";
                    break;

                case "2":
                    Title = "Setting";
                    break;

                case "3":
                    Title = "Réglage";
                    break;
            }
            BindingContext = _settingViewModel = new SettingViewModel();
        }
        private void Arabic_Tapped(object sender, EventArgs e)
        {
            CrossSecureStorage.Current.SetValue("Language", "1");
            HelperManger.ShowToast("تم تغيير لغة التطبيق إلى العربية");
            //myshell.FlowDirection = FlowDirection.RightToLeft;
            (Application.Current).MainPage = new AppShell();
        }

        private void English_Tapped(object sender, EventArgs e)
        {
            CrossSecureStorage.Current.SetValue("Language", "2");
            HelperManger.ShowToast("The language of the application has been changed to English");
            //myshell.FlowDirection = FlowDirection.LeftToRight;
            (Application.Current).MainPage = new AppShell();
        }

        private void French_Tapped(object sender, EventArgs e)
        {
            CrossSecureStorage.Current.SetValue("Language", "3");
            HelperManger.ShowToast("La langue de l’application a été modifiée pour Français");
            //myshell.FlowDirection = FlowDirection.LeftToRight;
            (Application.Current).MainPage = new AppShell();
        }

        private void Switch_Toggled_Arabic(object sender, ToggledEventArgs e)
        {
            //((SettingViewModel)this.BindingContext).ArabicIsToggled=
            _settingViewModel.ArabicIsToggled = !_settingViewModel.ArabicIsToggled;
        }

        private void Switch_Toggled_English(object sender, ToggledEventArgs e)
        {
            _settingViewModel.EnglishIsToggled = !_settingViewModel.EnglishIsToggled;
        }

        private void Switch_Toggled_French(object sender, ToggledEventArgs e)
        {
            _settingViewModel.FrenchIsToggled = !_settingViewModel.FrenchIsToggled;
        }
    }
}