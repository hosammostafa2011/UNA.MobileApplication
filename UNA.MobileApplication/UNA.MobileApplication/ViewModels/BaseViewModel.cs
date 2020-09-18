using System;
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

namespace UNA.MobileApplication.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();
        public IUserDialogs PageDialog = UserDialogs.Instance;
        public IMobileApiManager ApiManager;
        IApiService<IMobileApiManager> makeUpApi = new ApiService<IMobileApiManager>(Constant.ApiUrl);
        public ResourceDictionary Resources = new ResourceDictionary();
        public ObservableCollection<RESPONSE> _RESPONSE { get; set; } = new ObservableCollection<RESPONSE>();
        public REQUEST _REQUEST = new REQUEST("","");
        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
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
        #endregion

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

    }
}
