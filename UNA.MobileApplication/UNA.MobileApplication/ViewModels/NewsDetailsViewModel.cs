﻿using Helper;
using Newtonsoft.Json;
using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
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
        public Command FontMinusCommand { get; set; }
        public Command FontPlusCommand { get; set; }

        public int FontSize { get; set; }

        public NewsDetailsViewModel(NEWS item = null)
        {
            if (CrossSecureStorage.Current.HasKey("FontSize"))
            {
                FontSize = Convert.ToInt32(CrossSecureStorage.Current.GetValue("FontSize"));
            }
            else
            {
                FontSize = 20;
                CrossSecureStorage.Current.SetValue("FontSize", FontSize.ToString());
            }

            LoadDetailsCommand = new Command(async () => await RunSafe(ExecuteLoadDetailsCommandAsync(item), true));

            FontMinusCommand = new Command(async () => await FontMinus());
            FontPlusCommand = new Command(async () => await FontPlus());
            SelectedNews = item;
            if (string.IsNullOrEmpty(item.Details))
                LoadDetailsCommand.Execute(null);
            string favImage = string.Empty;
            try
            {
                favImage = GetFavouriteImage(item.News_ID);
            }
            catch (Exception)
            {
                favImage = "star.png";
            }

            if (SelectedNews.FavouriteImage == null || SelectedNews.FavouriteImage != favImage)
            {
                SelectedNews.FavouriteImage = favImage;
                NotifyPropertyChanged(nameof(SelectedNews));
            }

            NotifyPropertyChanged(nameof(FontSize));
        }

        private async Task FontMinus()
        {
            --FontSize;
            CrossSecureStorage.Current.SetValue("FontSize", FontSize.ToString());
            NotifyPropertyChanged(nameof(FontSize));
        }

        private async Task FontPlus()
        {
            ++FontSize;
            CrossSecureStorage.Current.SetValue("FontSize", FontSize.ToString());
            NotifyPropertyChanged(nameof(FontSize));
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

            var result = string.Empty;
            if (objNEWS.Category_ID == "8000")//reports
            {
                REPORT objREPORT = new REPORT();
                objREPORT.REPORT_ID = objNEWS.News_ID;
                _REQUEST.JSON = JsonConvert.SerializeObject(objREPORT);
                result = await ApiManager.GET_REPORT_DETAIL(_REQUEST);
            }
            else
            {
                _REQUEST.JSON = JsonConvert.SerializeObject(objNEWS);
                result = await ApiManager.GET_NEWS_DETAIL(_REQUEST);
            }
            _RESPONSE = HelperManger.CastToResponse(result);
            if (string.IsNullOrEmpty(_RESPONSE[0].ERROR_MESSAGE))
            {
                List<NEWS> lstNEWS = JsonConvert.DeserializeObject<List<NEWS>>(_RESPONSE[0].JSON);
                if (lstNEWS.Count > 0)
                {
                    if (string.IsNullOrEmpty(SelectedNews.Details))
                    {
                        SelectedNews.Details = HtmlToPlainText(lstNEWS[0].Details);
                        NotifyPropertyChanged(nameof(SelectedNews));
                    }
                }
            }
        }
    }
}