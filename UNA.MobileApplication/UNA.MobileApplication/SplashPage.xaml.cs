using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UNA.MobileApplication
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplashPage : ContentPage
    {
        public SplashPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ScaleIcon();
        }

        private async void ScaleIcon()
        {
            // wait until the UI is present

            // animate the splash logo
            await SplashIcon.ScaleTo(0.5, 1000, Easing.CubicInOut);
            var animationTasks = new[]{
                SplashIcon.ScaleTo(100.0, 2000, Easing.CubicInOut),
                SplashIcon.FadeTo(0, 1400, Easing.CubicInOut)
            };
            await Task.WhenAll(animationTasks);

            //// navigate to main page
            Application.Current.MainPage = new AppShell();
        }
    }
}