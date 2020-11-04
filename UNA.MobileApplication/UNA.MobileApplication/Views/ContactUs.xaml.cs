using Helper.Model;
using Plugin.SecureStorage;
using System;
using UNA.MobileApplication.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UNA.MobileApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactUs : ContentPage
    {
        private readonly ContactUsViewModel contactUsViewModel = null;

        public ContactUs(string categoryID, string categoryName, string nationID)
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
                    Title = "اتصل بنا";
                    break;

                case "2":
                    Title = "Contact Us";
                    break;

                case "3":
                    Title = "Contactez-nous";
                    break;
            }
            BindingContext = contactUsViewModel = new ContactUsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Shell.SetNavBarIsVisible(this, true);
            if (contactUsViewModel.obsContactData.Count <= 0)
                contactUsViewModel.LoadContactDataCommand.Execute(null);
        }

        private void FaceBook_Tapped(object sender, System.EventArgs e)
        {
            try
            {
                string uri = ((CONTACT)contactUsViewModel.obsContactData[6]).contact_title;
                Browser.OpenAsync(uri, new BrowserLaunchOptions
                {
                    LaunchMode = BrowserLaunchMode.SystemPreferred,
                    TitleMode = BrowserTitleMode.Show,
                });
            }
            catch (System.Exception ex)
            {
            }
        }

        private void Twitter_Tapped(object sender, System.EventArgs e)
        {
            try
            {
                string uri = ((CONTACT)contactUsViewModel.obsContactData[7]).contact_title;
                Browser.OpenAsync(uri, new BrowserLaunchOptions
                {
                    LaunchMode = BrowserLaunchMode.SystemPreferred,
                    TitleMode = BrowserTitleMode.Show,
                });
            }
            catch (System.Exception ex)
            {
            }
        }

        private void Instegram_Tapped(object sender, System.EventArgs e)
        {
            try
            {
                string uri = ((CONTACT)contactUsViewModel.obsContactData[9]).contact_title;
                Browser.OpenAsync(uri, new BrowserLaunchOptions
                {
                    LaunchMode = BrowserLaunchMode.SystemPreferred,
                    TitleMode = BrowserTitleMode.Show,
                });
            }
            catch (System.Exception ex)
            {
            }
        }

        /*private void GooglePlus_Tapped(object sender, System.EventArgs e)
        {
            try
            {
                string uri = ((CONTACT)contactUsViewModel.obsContactData[10]).contact_title;
                Browser.OpenAsync(uri, new BrowserLaunchOptions
                {
                    LaunchMode = BrowserLaunchMode.SystemPreferred,
                    TitleMode = BrowserTitleMode.Show,
                });
            }
            catch (System.Exception ex)
            {
            }
        }*/

        private void YouTube_Tapped(object sender, System.EventArgs e)
        {
            try
            {
                string uri = ((CONTACT)contactUsViewModel.obsContactData[8]).contact_title;
                Browser.OpenAsync(uri, new BrowserLaunchOptions
                {
                    LaunchMode = BrowserLaunchMode.SystemPreferred,
                    TitleMode = BrowserTitleMode.Show,
                });
            }
            catch (System.Exception ex)
            {
            }
        }
    }
}