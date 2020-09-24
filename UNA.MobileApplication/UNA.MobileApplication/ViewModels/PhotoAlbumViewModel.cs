using Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace UNA.MobileApplication.ViewModels
{
    public class PhotoAlbumViewModel : BaseViewModel
    {

        private ObservableCollection<PHOTO_ALBUM> _photoAlbum;

        public ObservableCollection<PHOTO_ALBUM> obsCollectionPHOTO_ALBUM
        {
            get => _photoAlbum;
            set => SetProperty(ref _photoAlbum, value);
        }

        public Command LoadPHOTO_ALBUMCommand { get; set; }

        public PhotoAlbumViewModel()
        {
            obsCollectionPHOTO_ALBUM = new ObservableCollection<PHOTO_ALBUM>();
            LoadPHOTO_ALBUMCommand = new Command(async () => await RunSafe(ExecuteLoadItemsCommandAsync(), true));
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
                var result = await ApiManager.GET_PHOTO_ALBUM(_REQUEST);
                _RESPONSE = HelperManger.CastToResponse(result);
                if (string.IsNullOrEmpty(_RESPONSE[0].ERROR_MESSAGE))
                {
                    List<PHOTO_ALBUM> lstPHOTO_ALBUM = JsonConvert.DeserializeObject<List<PHOTO_ALBUM>>(_RESPONSE[0].JSON);
                    obsCollectionPHOTO_ALBUM = new ObservableCollection<PHOTO_ALBUM>(lstPHOTO_ALBUM);
                }
                NotifyPropertyChanged(nameof(obsCollectionPHOTO_ALBUM));
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