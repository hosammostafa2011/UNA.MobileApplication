using Acr.UserDialogs;
using Helper;
using Plugin.SecureStorage;
using Plugin.Share;
using Plugin.Share.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
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
            NavigationPage.SetHasNavigationBar(this, false);


            BindingContext = newsDetailsViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //imageContainer.FadeTo(1, 200, Easing.CubicInOut);
            //imageContainer.TranslateTo(0, 0, 200, Easing.CubicInOut);
            detailContainer.FadeTo(1, 200, Easing.CubicInOut);
            detailContainer.TranslateTo(0, 0, 200, Easing.CubicInOut);
            descriptionContainer.FadeTo(1, 350, Easing.CubicInOut);
            descriptionContainer.TranslateTo(0, 0, 350, Easing.CubicInOut);

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

                    HelperManger.ShowToast("تم ازالة الخبر من المفضلة");
                }
                else
                {
                    FavouriteList.Add(NewsId);
                    imgFavourite.Source = "star_sel.png";
                    HelperManger.ShowToast("تم اضافة الخبر الى المفضلة");
                }
                strFavouriteList = String.Join(",", FavouriteList);
                CrossSecureStorage.Current.SetValue("FavouriteList", strFavouriteList);
                await Application.Current.SavePropertiesAsync();
            }
            else
            {
                CrossSecureStorage.Current.SetValue("FavouriteList", NewsId);
                HelperManger.ShowToast("تم اضافة الخبر الى المفضلة");
                imgFavourite.Source = "star_sel.png";
            }
        }

        private void imgShare_Tapped(object sender, EventArgs e)
        {
            IShare shareInfo = CrossShare.Current;
            ShareMessage _ShareMessage = new ShareMessage();
            _ShareMessage.Text = lblTitle.Text;
            if (((NewsDetailsViewModel)this.BindingContext).SelectedNews.Category_ID == "8000")
                _ShareMessage.Url = string.Format(Constant.ReportsURL, lblNews_ID.Text);
            else
                _ShareMessage.Url = string.Format(Constant.NewsURL, lblNews_ID.Text);
            shareInfo.Share(_ShareMessage);
        }
    }
}