using Helper;
using Helper.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace UNA.MobileApplication.ViewModels
{
    public class VideoAlbumViewModel : BaseViewModel
    {

        private ObservableCollection<VIDEO> _video;

        public ObservableCollection<VIDEO> obsCollectionVIDEO
        {
            get => _video;
            set => SetProperty(ref _video, value);
        }

        public Command LoadVIDEOCommand { get; set; }

        public VideoAlbumViewModel()
        {
            obsCollectionVIDEO = new ObservableCollection<VIDEO>();
            LoadVIDEOCommand = new Command(async () => await RunSafe(ExecuteLoadItemsCommandAsync(), true));
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
                _REQUEST.ROW_COUNT = "50";
                var result = await ApiManager.GET_VIDEO(_REQUEST);
                _RESPONSE = HelperManger.CastToResponse(result);
                if (string.IsNullOrEmpty(_RESPONSE[0].ERROR_MESSAGE))
                {
                    List<VIDEO> lstVIDEO = JsonConvert.DeserializeObject<List<VIDEO>>(_RESPONSE[0].JSON);
                    obsCollectionVIDEO = new ObservableCollection<VIDEO>(lstVIDEO);
                }
                NotifyPropertyChanged(nameof(obsCollectionVIDEO));
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