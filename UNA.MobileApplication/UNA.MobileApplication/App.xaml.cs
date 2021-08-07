using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using UNA.MobileApplication.Services;
using UNA.MobileApplication.Views;
using Plugin.SecureStorage;

[assembly: ExportFont("HelveticaNeue.ttf", Alias = "HelveticaNeue")]

namespace UNA.MobileApplication
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //DependencyService.Register<MockDataStore>();
            try
            {                
                if (string.IsNullOrEmpty(CrossSecureStorage.Current.GetValue("Language")))
                    CrossSecureStorage.Current.SetValue("Language", "1");
            }
            catch (Exception)
            {
                CrossSecureStorage.Current.SetValue("Language", "1");
            }
            MainPage = new SplashPage();
            /*if (Device.RuntimePlatform == Device.iOS)
            {
                MainPage = new SplashPage();
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                MainPage = new SplashPage();
                //MainPage = new AppShell();
            }*/
        }

        protected override void OnStart()
        {
            base.OnStart();
        }

        protected override void OnSleep()
        {
            base.OnSleep();
        }

        protected override void OnResume()
        {
            base.OnResume();
        }
    }
}