using Helper;
using Newtonsoft.Json;
using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace UNA.MobileApplication.ViewModels
{
    public class NewsDetailsViewModel : BaseViewModel
    {
        private NEWS _selectedNews;

        public NEWS SelectedNews
        {
            get { return _selectedNews; }
            set => SetProperty(ref _selectedNews, value);
        }

        public Command LoadDetailsCommand { get; set; }

        public NewsDetailsViewModel(NEWS item = null)
        {
            LoadDetailsCommand = new Command(async () => await RunSafe(ExecuteLoadDetailsCommandAsync(item), true));
            SelectedNews = item;
            if (string.IsNullOrEmpty(item.Details))
                LoadDetailsCommand.Execute(null);
            string favImage = GetFavouriteImage(item.News_ID);
            if (SelectedNews.FavouriteImage != favImage)
            {
                SelectedNews.FavouriteImage = favImage;
                NotifyPropertyChanged(nameof(SelectedNews));
            }
        }

        private string GetFavouriteImage(string pNews_ID)
        {
            if (CrossSecureStorage.Current.HasKey("FavouriteList"))
            {
                string strFavouriteList = CrossSecureStorage.Current.GetValue("FavouriteList");
                List<string> FavouriteList = strFavouriteList.Split(',').ToList();
                if (FavouriteList.Contains(pNews_ID))
                {
                    return "star_sel.png";
                }
                else
                {
                    return "star.png";
                }
            }
            else
            {
                return "star.png";
            }
        }

        private async Task ExecuteLoadDetailsCommandAsync(NEWS objNEWS)
        {
            try
            {
                if (CrossSecureStorage.Current.HasKey("Language"))
                    _REQUEST.LANGUAGE = CrossSecureStorage.Current.GetValue("Language");
                else
                    _REQUEST.LANGUAGE = "1";
            }
            catch (Exception)
            {
                _REQUEST.LANGUAGE = "1";
            }
            _REQUEST.USER_TOKEN = "Aa159357";
            _REQUEST.JSON = JsonConvert.SerializeObject(objNEWS);
            var result = await ApiManager.GET_NEWS_DETAIL(_REQUEST);
            _RESPONSE = HelperManger.CastToResponse(result);
            if (string.IsNullOrEmpty(_RESPONSE[0].ERROR_MESSAGE))
            {
                List<NEWS> lstNEWS = JsonConvert.DeserializeObject<List<NEWS>>(_RESPONSE[0].JSON);
                if (lstNEWS.Count > 0)
                {
                    SelectedNews.Details = HtmlToPlainText(lstNEWS[0].Details);
                    NotifyPropertyChanged(nameof(SelectedNews));
                }
            }
        }
    }
}