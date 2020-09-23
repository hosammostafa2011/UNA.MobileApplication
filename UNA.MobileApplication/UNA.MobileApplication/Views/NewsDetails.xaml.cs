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
    public partial class NewsDetails : ContentPage
    {
        public NewsDetails(NewsDetailsViewModel newsDetailsViewModel)
        {
            InitializeComponent();
            BindingContext = newsDetailsViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            detailContainer.FadeTo(1, 200, Easing.CubicInOut);
            detailContainer.TranslateTo(0, 0, 200, Easing.CubicInOut);

            descriptionContainer.FadeTo(1, 350, Easing.CubicInOut);
            descriptionContainer.TranslateTo(0, 0, 350, Easing.CubicInOut);
            Shell.SetNavBarIsVisible(this, false);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () => await Navigation.PopAsync());
        }

        private async void imgFavourite_Tapped(object sender, EventArgs e)
        {
            string NewsId = lblNews_ID.Text;
            if (CrossSecureStorage.Current.HasKey("FavouriteList"))
            {
                string strFavouriteList = CrossSecureStorage.Current.GetValue("FavouriteList");
                List<string> FavouriteList = strFavouriteList.Split(',').ToList();
                if (FavouriteList.Contains(NewsId))
                {
                    FavouriteList.Remove(NewsId);
                    imgFavourite.Source = "star.png";
                }
                else
                {
                    FavouriteList.Add(NewsId);
                    imgFavourite.Source = "star_sel.png";
                }
                strFavouriteList = String.Join(",", FavouriteList);
                CrossSecureStorage.Current.SetValue("FavouriteList", strFavouriteList);
                await Application.Current.SavePropertiesAsync();
            }
            else
            {
                CrossSecureStorage.Current.SetValue("FavouriteList", NewsId);
                imgFavourite.Source = "star_sel.png";
            }
        }
    }
}