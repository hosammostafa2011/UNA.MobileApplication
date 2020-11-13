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
                    lblAppLang.Text = "لغة التطبيق";
                    lblNotifyLang.Text = "لغة الإشعارات";
                    btnSave.Text = "حفــظ";
                    break;
                case "2":
                    Title = "Setting";
                    lblAppLang.Text = "Language";
                    lblNotifyLang.Text = "Notification language";
                    btnSave.Text = "Save";
                    break;
                case "3":
                    Title = "Réglage";
                    lblAppLang.Text = "Langue";
                    lblNotifyLang.Text = "Langue de notification";
                    btnSave.Text = "Enregistrer";
                    break;
            }
            BindingContext = _settingViewModel = new SettingViewModel();
        }
        private void Arabic_Tapped(object sender, EventArgs e)
        {
            CrossSecureStorage.Current.SetValue("Language", "1");
            HelperManger.ShowToast("تم تغيير لغة التطبيق إلى العربية");
            if (Device.RuntimePlatform == Device.Android)
                (Application.Current).MainPage = new AppShell();
            else
                (Application.Current).MainPage = new RootPage();
        }

        private void English_Tapped(object sender, EventArgs e)
        {
            CrossSecureStorage.Current.SetValue("Language", "2");
            HelperManger.ShowToast("The language of the application has been changed to English");
            if (Device.RuntimePlatform == Device.Android)
                (Application.Current).MainPage = new AppShell();
            else
                (Application.Current).MainPage = new RootPage();
        }

        private void French_Tapped(object sender, EventArgs e)
        {
            CrossSecureStorage.Current.SetValue("Language", "3");
            HelperManger.ShowToast("La langue de l’application a été modifiée pour Français");
            if (Device.RuntimePlatform == Device.Android)
                (Application.Current).MainPage = new AppShell();
            else
                (Application.Current).MainPage = new RootPage();
        }

        private void Switch_Toggled_Arabic(object sender, ToggledEventArgs e)
        {
            //((SettingViewModel)this.BindingContext).ArabicIsToggled=
            if (chkArabic.IsToggled)
            {
                chkEnglish.IsToggled = chkFrench.IsToggled = false;
                _settingViewModel.ArabicIsToggled = true;
                _settingViewModel.EnglishIsToggled = _settingViewModel.FrenchIsToggled = false;
            }
        }

        private void Switch_Toggled_English(object sender, ToggledEventArgs e)
        {
            if (chkEnglish.IsToggled)
            {
                chkArabic.IsToggled = chkFrench.IsToggled = false;
                _settingViewModel.EnglishIsToggled = true;
                _settingViewModel.ArabicIsToggled = _settingViewModel.FrenchIsToggled = false;
            }
        }

        private void Switch_Toggled_French(object sender, ToggledEventArgs e)
        {
            if (chkFrench.IsToggled)
            {
                chkArabic.IsToggled = chkEnglish.IsToggled = false;
                _settingViewModel.FrenchIsToggled = true;
                _settingViewModel.ArabicIsToggled = _settingViewModel.EnglishIsToggled = false;
            }
        }
    }
}