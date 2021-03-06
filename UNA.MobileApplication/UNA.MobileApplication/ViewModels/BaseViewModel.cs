﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using UNA.MobileApplication.Models;
using UNA.MobileApplication.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Acr.UserDialogs;
using System.Diagnostics;
using Helper.Service;
using Helper.Interface;
using Helper;
using Model.Mobile;
using System.Text.RegularExpressions;
using Plugin.SecureStorage;
using Xamarin.Essentials;

namespace UNA.MobileApplication.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();
        public IUserDialogs PageDialog = UserDialogs.Instance;
        public IMobileApiManager ApiManager;
        private IApiService<IMobileApiManager> makeUpApi = new ApiService<IMobileApiManager>(Constant.ApiUrl);
        public ResourceDictionary Resources = new ResourceDictionary();
        public ObservableCollection<RESPONSE> _RESPONSE { get; set; } = new ObservableCollection<RESPONSE>();

        private string USER_LANGUAGE = CrossSecureStorage.Current.GetValue("Language");
        private FlowDirection DEVICE_DIRECTION = Device.FlowDirection;

        //public REQUEST _REQUEST = new REQUEST("", "");
        public REQUEST _REQUEST = new REQUEST();
        public static string FCM_TOKEN { get { return CrossSecureStorage.Current.GetValue("FCMToken"); } }
        private bool isBusy = false;

        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        private string title = string.Empty;

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion INotifyPropertyChanged

        //-------------------------
        public BaseViewModel()
        {
            ApiManager = new MobileApiManager(makeUpApi);
        }

        public BaseViewModel(bool listenCultureChanges = false)
        {
            if (listenCultureChanges)
            {
                // Listen culture changes so they can be handled
                // by derived viewmodels if needed
                //_notifier = new CultureChangeNotifier();
                //_notifier.CultureChanged += CultureChanged;
            }
            ApiManager = new MobileApiManager(makeUpApi);
        }

        public async Task RunSafe(Task task, bool ShowLoading = true, string loadinMessage = null)
        {
            try
            {
                if (IsBusy) return;

                IsBusy = true;

                if (ShowLoading) UserDialogs.Instance.ShowLoading(loadinMessage ?? "Loading");

                await task;
            }
            catch (Exception e)
            {
                IsBusy = false;
                UserDialogs.Instance.HideLoading();
                Debug.WriteLine(e.ToString());
                // await App.Current.MainPage.DisplayAlert("Eror", e.ToString(), "Ok");
            }
            finally
            {
                IsBusy = false;
                if (ShowLoading) UserDialogs.Instance.HideLoading();
            }
        }

        private TextAlignment _horizontalDirection;

        public TextAlignment HorizontalDirection
        {
            get
            {
                /*
                // get the device language and the current seelct user language and compare
                //FlowDirection xx = DEVICE_DIRECTION;
                if (DEVICE_DIRECTION.Equals(FlowDirection.RightToLeft)) // Arabic
                {
                    if (USER_LANGUAGE.Equals("1"))
                    {
                        _horizontalDirection = Device.RuntimePlatform == Device.iOS ? TextAlignment.Start : TextAlignment.End;
                    }
                    else
                    {
                        _horizontalDirection = Device.RuntimePlatform == Device.iOS ? TextAlignment.End : TextAlignment.Start;
                    }
                }
                else
                {
                    if (USER_LANGUAGE.Equals("1"))
                    {
                        _horizontalDirection = Device.RuntimePlatform == Device.iOS ? TextAlignment.End : TextAlignment.Start;
                    }
                    else
                    {
                        _horizontalDirection = Device.RuntimePlatform == Device.iOS ? TextAlignment.Start : TextAlignment.End;
                    }
                }*/
                _horizontalDirection = TextAlignment.Start;
                return _horizontalDirection;
            }
        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void NotifyAllPropertiesChanged()
        {
            NotifyPropertyChanged(null);
        }

        public static string HtmlToPlainText(string html)
        {
            const string tagWhiteSpace = @"(>|$)(\W|\n|\r)+<";//matches one or more (white space or line breaks) between '>' and '<'
            const string stripFormatting = @"<[^>]*(>|$)";//match any character between '<' and '>', even when end tag is missing
            const string lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>";//matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />
            var lineBreakRegex = new Regex(lineBreak, RegexOptions.Multiline);
            var stripFormattingRegex = new Regex(stripFormatting, RegexOptions.Multiline);
            var tagWhiteSpaceRegex = new Regex(tagWhiteSpace, RegexOptions.Multiline);

            var text = html;
            text = System.Net.WebUtility.HtmlDecode(text);
            text = tagWhiteSpaceRegex.Replace(text, "><");
            text = lineBreakRegex.Replace(text, Environment.NewLine);
            text = stripFormattingRegex.Replace(text, string.Empty);
            //string fontSize = "14";
            //text = "<html><head>" +
            //    "<link href='https://fonts.googleapis.com/css2?family=Cairo:wght@300&display=swap' rel='stylesheet'>"
            //    + "</head>"
            //    + "<body >" +
            //String.Format("<p style=\"text-align:justify;font-family:HelveticaNeue;font-size:20px;\">{0}</p>", text) +
            //                "</body></html>";

            return text;
        }
    }
}