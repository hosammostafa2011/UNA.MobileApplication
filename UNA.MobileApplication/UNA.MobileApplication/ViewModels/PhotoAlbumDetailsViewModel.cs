﻿using Helper;
using Newtonsoft.Json;
using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace UNA.MobileApplication.ViewModels
{
    public class PhotoAlbumDetailsViewModel : BaseViewModel
    {
        public ObservableCollection<PHOTO_ALBUM> _PhotoAlbumDetails;
        public PHOTO_ALBUM SELECTED_PHOTO_ALBUM { get; set; }
        public ObservableCollection<PHOTO_ALBUM> obsCollectionPHOTO_ALBUM
        {
            get => _PhotoAlbumDetails;
            set => SetProperty(ref _PhotoAlbumDetails, value);
        }
        
        public Command LoadPHOTO_ALBUMCommand { get; set; }

        public PhotoAlbumDetailsViewModel(PHOTO_ALBUM item)
        {
            obsCollectionPHOTO_ALBUM = new ObservableCollection<PHOTO_ALBUM>();
            LoadPHOTO_ALBUMCommand = new Command(async () => await RunSafe(ExecuteLoadItemsCommandAsync(item), true));
            LoadPHOTO_ALBUMCommand.Execute(item);
        }

        private async Task ExecuteLoadItemsCommandAsync(PHOTO_ALBUM objPHOTO_ALBUM)
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
                _REQUEST.JSON = JsonConvert.SerializeObject(objPHOTO_ALBUM);
                var result = await ApiManager.GET_PHOTO_ALBUM_DETAIL(_REQUEST);
                _RESPONSE = HelperManger.CastToResponse(result);
                if (string.IsNullOrEmpty(_RESPONSE[0].ERROR_MESSAGE))
                {
                    List<PHOTO_ALBUM> lstPHOTO_ALBUM = JsonConvert.DeserializeObject<List<PHOTO_ALBUM>>(_RESPONSE[0].JSON);
                    obsCollectionPHOTO_ALBUM = new ObservableCollection<PHOTO_ALBUM>(lstPHOTO_ALBUM);
                    SELECTED_PHOTO_ALBUM = objPHOTO_ALBUM;
                    NotifyPropertyChanged(nameof(SELECTED_PHOTO_ALBUM));
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