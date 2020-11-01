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
            logoImage.TranslateTo(0, -300, 0);
            brand.TranslateTo((-500 - brand.Bounds.Width), 0, 0);
            worldmap.TranslateTo((500 + worldmap.Bounds.Width), 0, 0);
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
            await Task.Delay(1000);
            var animationTasks = new[]{
                 logoImage.TranslateTo(0, 0, 1500),
             brand.TranslateTo(0, 0, 1500),
             worldmap.TranslateTo(0, 0, 1500)
        };
            await Task.WhenAll(animationTasks);

            if (Device.RuntimePlatform == Device.iOS)
                Application.Current.MainPage = new RootPage();
            else
                Application.Current.MainPage = new AppShell();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            ScaleIcon();
        }
    }
}