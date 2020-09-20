using Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace UNA.MobileApplication.ViewModels
{
    public class NewsListViewModel : BaseViewModel
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
        public Command LoadNewsCommand { get; set; }


        public NewsListViewModel(string categoryID)
        {
            obsCollectionNews = new ObservableCollection<NEWS>();
            LoadNewsCommand = new Command(async () => await RunSafe(ExecuteLoadItemsCommandAsync(categoryID), true));
        }
        async Task ExecuteLoadItemsCommandAsync(string categoryID)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                _REQUEST.LANGUAGE = "1";
                _REQUEST.USER_TOKEN = "Aa@159357";
                CATEGORY objCATEGORY = new CATEGORY();
                objCATEGORY.CATEGORY_ID = "12";
                _REQUEST.ROW_COUNT = "10";
                _REQUEST.JSON = JsonConvert.SerializeObject(objCATEGORY);
                var result = await ApiManager.GET_NEWS_BY_CATEGORY(_REQUEST);
                _RESPONSE = HelperManger.CastToResponse(result);
                if (string.IsNullOrEmpty(_RESPONSE[0].ERROR_MESSAGE))
                {
                    List<NEWS> lstNEWS = JsonConvert.DeserializeObject<List<NEWS>>(_RESPONSE[0].JSON);
                    obsCollectionNews = new ObservableCollection<NEWS>(lstNEWS);
                }
                NotifyPropertyChanged(nameof(obsCollectionNews));
                //obsCollectionNews = new ObservableCollection<NEWS>(new List<NEWS>
                //{
                //    new NEWS {NEWS_ID = "1",TITLE="Test 1",  FROM_DATE = "12/12/2020", ImageUrl    = "http://www.una-oic.org//UploadDir//4b7a5351-f3a3-4567-a81a-4292242aa97f.jpg"},
                //    new NEWS {NEWS_ID = "2",TITLE="Test 2", FROM_DATE = "12/12/2020" , ImageUrl         = "http://www.una-oic.org//UploadDir//4b7a5351-f3a3-4567-a81a-4292242aa97f.jpg"},
                //    new NEWS {NEWS_ID = "3", TITLE="Test 3",FROM_DATE = "12/12/2020", ImageUrl         = "http://www.una-oic.org//UploadDir//4b7a5351-f3a3-4567-a81a-4292242aa97f.jpg"},
                //});

            }
            catch (Exception ex)
            {
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
