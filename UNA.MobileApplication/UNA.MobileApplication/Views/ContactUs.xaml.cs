using Helper.Model;
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
        public ContactUs()
        {
            InitializeComponent();
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