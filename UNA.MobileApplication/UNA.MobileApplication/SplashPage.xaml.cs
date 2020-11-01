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
            /*await SplashIcon.ScaleTo(0.5, 500, Easing.CubicInOut);
            var animationTasks = new[]{
                SplashIcon.ScaleTo(100.0, 1000, Easing.CubicInOut),
                SplashIcon.FadeTo(0, 700, Easing.CubicInOut)
            };*/
            var animationTasks = new[]{
                logoImage.TranslateTo (-100, -100, 1000),
                centerImage.TranslateTo (0, 100, 1000)
        };
            await Task.WhenAll(animationTasks);

            if (Device.RuntimePlatform == Device.iOS)
                Application.Current.MainPage = new RootPage();
            else
                Application.Current.MainPage = new AppShell();
        }
    }
}