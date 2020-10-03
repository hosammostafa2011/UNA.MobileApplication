using Helper;
using Newtonsoft.Json;
using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace UNA.MobileApplication.ViewModels
{
    public class NationViewModel : BaseViewModel
    {
        private ObservableCollection<NATION> _NATION;

        public ObservableCollection<NATION> obsCollectionNATION
        {
            get => _NATION;
            set => SetProperty(ref _NATION, value);
        }

        public Command LoadNATIONCommand { get; set; }

        public ICommand PerformSearch => new Command<string>((string query) =>
        {
            obsCollectionNATION = GetSearchResults(query);
        });

        private ObservableCollection<NATION> GetSearchResults(string query)
        {
            ObservableCollection<NATION> _temp = new ObservableCollection<NATION>();
            foreach (NATION _NATION in obsCollectionNATION)
            {
                if (_NATION.Nation_Name.Contains(query))
                    _temp.Add(_NATION);
            }
            return _temp;
        }

        public NationViewModel()
        {
            obsCollectionNATION = new ObservableCollection<NATION>();
            LoadNATIONCommand = new Command(async () => await RunSafe(ExecuteLoadItemsCommandAsync(), true));
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
                    _REQUEST.LANGUAGE = CrossSecureStorage.Current.GetValue("Language");
                }
                catch (Exception)
                {
                    _REQUEST.LANGUAGE = "1";
                }
                _REQUEST.USER_TOKEN = "Aa159357";
                _REQUEST.ROW_COUNT = "50";
                var result = await ApiManager.GET_NATION(_REQUEST);
                _RESPONSE = HelperManger.CastToResponse(result);
                if (string.IsNullOrEmpty(_RESPONSE[0].ERROR_MESSAGE))
                {
                    List<NATION> lstNATION = JsonConvert.DeserializeObject<List<NATION>>(_RESPONSE[0].JSON);
                    obsCollectionNATION = new ObservableCollection<NATION>(lstNATION);
                }
                NotifyPropertyChanged(nameof(obsCollectionNATION));
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