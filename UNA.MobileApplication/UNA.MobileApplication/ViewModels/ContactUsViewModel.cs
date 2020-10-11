using Helper.Model;
using Plugin.SecureStorage;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace UNA.MobileApplication.ViewModels
{
    public class ContactUsViewModel : BaseViewModel
    {
        private ObservableCollection<CONTACT> CONTACT_DATA;

        public ObservableCollection<CONTACT> obsContacts
        {
            get;
            set;
        }
        public ContactUsViewModel()
        {
            LoadContactDataCommand = new Command(async () =>
            await RunSafe(ExecuteLoadItemsCommandAsync(), true));
        }
        public Command LoadContactDataCommand { get; set; }

        private async Task ExecuteLoadItemsCommandAsync()
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
            var result = "";
            _REQUEST.JSON = string.Empty;

            NATION objNATION = new NATION();
            result = await ApiManager.GET_CONTACT(_REQUEST);

        }
    }

}