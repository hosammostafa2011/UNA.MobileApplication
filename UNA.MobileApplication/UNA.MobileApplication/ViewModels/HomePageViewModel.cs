using Helper;
using Newtonsoft.Json;
using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace UNA.MobileApplication.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        private NEWS _selectedNews;
        private bool _isRefreshing = false;
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

        public void VersionTracking(string currentVersion)
        {
        }

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsRefreshing = true;

                    await RunSafe(ExecuteLoadItemsCommandAsync(), true);

                    IsRefreshing = false;
                    NotifyPropertyChanged();
                });
            }
        }

        public HomePageViewModel()
        {
            obsCollectionNews = new ObservableCollection<NEWS>();
            Device.BeginInvokeOnMainThread(async () => await RunSafe(ExecuteLoadItemsCommandAsync(), true));
        }

        private async Task ExecuteLoadItemsCommandAsync()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            try
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
                _REQUEST.ROW_COUNT = "50";
                var result = await ApiManager.GET_LATEST_NEWS(_REQUEST);
                _RESPONSE = HelperManger.CastToResponse(result);
                if (string.IsNullOrEmpty(_RESPONSE[0].ERROR_MESSAGE))
                {
                    List<NEWS> lstNEWS = JsonConvert.DeserializeObject<List<NEWS>>(_RESPONSE[0].JSON);
                    obsCollectionNews = new ObservableCollection<NEWS>(lstNEWS);
                }
                foreach (NEWS vNEWS in obsCollectionNews)
                {
                    try
                    {
                        vNEWS.FavouriteImage = GetFavouriteImage(vNEWS.News_ID);
                    }
                    catch (Exception)
                    {
                        vNEWS.FavouriteImage = "star.png";
                    }
                    // direction
                    vNEWS.HorizontalDirection = HelperManger.GetTextAlignment(_REQUEST.LANGUAGE);
                }
                NotifyPropertyChanged(nameof(obsCollectionNews));
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