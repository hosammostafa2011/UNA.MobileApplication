using Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace UNA.MobileApplication.ViewModels
{
    public class NewsDetailsViewModel : BaseViewModel
    {
        private NEWS _selectedNews;
        private int LANGUAGE { get; set; } = 1;
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
        }
        private async Task ExecuteLoadDetailsCommandAsync(NEWS objNEWS)
        {
            _REQUEST.LANGUAGE = Convert.ToString(LANGUAGE);
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