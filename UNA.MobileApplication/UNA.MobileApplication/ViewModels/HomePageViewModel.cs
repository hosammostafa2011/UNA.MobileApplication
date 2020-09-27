using Helper;
using Newtonsoft.Json;
using Plugin.SecureStorage;
using Prism.Commands;
using Prism.Navigation;
using Prism.Navigation.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNA.MobileApplication.Models;
using Xamarin.Forms;

namespace UNA.MobileApplication.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        private NEWS _selectedNews;

        public string CategoryId { get; set; }

        public NEWS SelectedNews
        {
            get => _selectedNews;
            set => SetProperty(ref _selectedNews, value);
        }
        private ObservableCollection<NEWS> _news;
        public ObservableCollection<NEWS> obsCollectionNews
        {
            get => _news;
            set => SetProperty(ref _news, value);
        }

        public ObservableCollection<NEWS> obsCollectionNewsFirst { get; set; }
        public ObservableCollection<NEWS> obsCollectionNewsSecond { get; set; }
        public ObservableCollection<NEWS> obsCollectionNewsThird { get; set; }
        //public Command LoadHomeNewsCommand { get; set; }

        public HomePageViewModel()
        {
            obsCollectionNews = new ObservableCollection<NEWS>();
            Device.BeginInvokeOnMainThread(async () => await RunSafe(ExecuteLoadItemsCommandAsync(), true));
            //LoadHomeNewsCommand = new Command(async () => await RunSafe(ExecuteLoadItemsCommandAsync(), true));
        }
        private async Task ExecuteLoadItemsCommandAsync()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            try
            {
                _REQUEST.LANGUAGE = "1";
                _REQUEST.USER_TOKEN = "Aa159357";
                _REQUEST.ROW_COUNT = "10";
                var result = await ApiManager.GET_LATEST_NEWS(_REQUEST);
                _RESPONSE = HelperManger.CastToResponse(result);
                if (string.IsNullOrEmpty(_RESPONSE[0].ERROR_MESSAGE))
                {
                    List<NEWS> lstNEWS = JsonConvert.DeserializeObject<List<NEWS>>(_RESPONSE[0].JSON);
                    obsCollectionNews = new ObservableCollection<NEWS>(lstNEWS);
                }
                foreach (NEWS vNEWS in obsCollectionNews)
                {
                    //vNEWS.Details = HtmlToPlainText(vNEWS.Details);
                    vNEWS.FavouriteImage = GetFavouriteImage(vNEWS.News_ID); ;
                }
                NotifyPropertyChanged(nameof(obsCollectionNews));
                /*NotifyPropertyChanged(nameof(obsCollectionNewsFirst));
                NotifyPropertyChanged(nameof(obsCollectionNewsSecond));
                NotifyPropertyChanged(nameof(obsCollectionNewsThird));*/
            }
            catch (Exception ex)
            {
            }
            finally
            {
                IsBusy = false;
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
    }
}