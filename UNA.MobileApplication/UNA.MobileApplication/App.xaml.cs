using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using UNA.MobileApplication.Services;
using UNA.MobileApplication.Views;
[assembly: ExportFont("HelveticaNeue.ttf", Alias = "HelveticaNeue")]
namespace UNA.MobileApplication
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
