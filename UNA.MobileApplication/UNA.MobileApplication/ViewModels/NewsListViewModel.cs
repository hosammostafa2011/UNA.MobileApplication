﻿using Helper;
using Newtonsoft.Json;
using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace UNA.MobileApplication.ViewModels
{
    public class NewsListViewModel : BaseViewModel
    {
        private NEWS _selectedNews;
        private bool _isRefreshing = false;
        private int CURRENT_PAGE { get; set; } = 1;
        private int PAGE_SIZE { get; set; } = 30;
        private int LANGUAGE { get; set; } = 1;
        private int TOTAL_MAIL { get; set; } = 100000;

        public string CATEGORY_ID { get; set; }

        public NewsListViewModel(string categoryID)
        {
            CATEGORY_ID = categoryID;
            obsCollectionNews = new ObservableCollection<NEWS>();
            RegiesterMessageCenter();
            LoadNewsCommand = new Command(async () =>
            await RunSafe(ExecuteLoadItemsCommandAsync(categoryID, false, false), true));
        }

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

        private async Task ExecuteLoadItemsCommandAsync(string categoryID, bool isRefresh, bool ClearList)
        {
            if (IsBusy)
                return;
            IsBusy = true;
            if (ClearList)
            {
                CURRENT_PAGE = 1;
            }
            try
            {
                _REQUEST.LANGUAGE = Convert.ToString(LANGUAGE);
                _REQUEST.USER_TOKEN = "Aa159357";
                var result = "";
                _REQUEST.JSON = string.Empty;
                if (categoryID == "7000")
                {
                    if (CrossSecureStorage.Current.HasKey("FavouriteList"))
                    {
                        NEWS objNews = new NEWS();
                        objNews.FAVOURITE_IDS = CrossSecureStorage.Current.GetValue("FavouriteList");
                        _REQUEST.JSON = JsonConvert.SerializeObject(objNews);
                        result = await ApiManager.GET_FAVOURITE(_REQUEST);
                    }
                }
                else if (categoryID == "8000")
                {
                    _REQUEST.ROW_COUNT = Convert.ToString(50);
                    result = await ApiManager.GET_REPORT(_REQUEST);
                }
                else
                {
                    CATEGORY objCATEGORY = new CATEGORY();
                    objCATEGORY.Category_ID = categoryID;
                    _REQUEST.ROW_COUNT = Convert.ToString(PAGE_SIZE);
                    _REQUEST.PAGE_NUMBER = Convert.ToString(CURRENT_PAGE);
                    _REQUEST.JSON = JsonConvert.SerializeObject(objCATEGORY);
                    result = await ApiManager.GET_NEWS_BY_CATEGORY(_REQUEST);
                }
                if (!string.IsNullOrEmpty(result))
                {
                    _RESPONSE = HelperManger.CastToResponse(result);
                    if (string.IsNullOrEmpty(_RESPONSE[0].ERROR_MESSAGE))
                    {
                        List<NEWS> lstNEWS = JsonConvert.DeserializeObject<List<NEWS>>(_RESPONSE[0].JSON);
                        obsCollectionNews = new ObservableCollection<NEWS>(lstNEWS);
                    }
                    foreach (NEWS vNEWS in obsCollectionNews)
                    {
                        vNEWS.Details = HtmlToPlainText(vNEWS.Details);
                        vNEWS.FavouriteImage = GetFavouriteImage(vNEWS.News_ID); ;
                    }
                    NotifyPropertyChanged(nameof(obsCollectionNews));
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                IsBusy = false;
                if (isRefresh)
                    HelperManger.ShowToast("تم تحديث قائمة الأخبار");
            }
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

                    await RunSafe(ExecuteLoadItemsCommandAsync(CATEGORY_ID, true, true), false);

                    IsRefreshing = false;
                    NotifyPropertyChanged();
                });
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

        private void RegiesterMessageCenter()
        {
            MessagingCenter.Subscribe<string, NEWS>("MyApp", "LoadNews", async (sender, arg) =>
            {
                int itemPostion = obsCollectionNews.IndexOf(arg);

                if (
                (itemPostion < (CURRENT_PAGE * PAGE_SIZE) - 1 && itemPostion < TOTAL_MAIL - 1)
                || itemPostion == TOTAL_MAIL - 1)
                {
                    return;
                }
                else
                {
                    CURRENT_PAGE = CURRENT_PAGE + 1;
                    await RunSafe(ExecuteLoadItemsCommandAsync(CATEGORY_ID, false, false), true);
                    //await GetInboxData(false);
                }
            });
        }
    }
}