using Helper;
using Plugin.SecureStorage;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace UNA.MobileApplication.ViewModels
{
    public class SettingViewModel : BaseViewModel
    {
        public SettingViewModel()
        {
            Device.BeginInvokeOnMainThread(async () => await RunSafe(GetNotificationAsync(), true));
        }

        private async Task GetNotificationAsync()
        {
            _REQUEST.USER_TOKEN = "Aa159357";
            var result = "";
            _REQUEST.JSON = string.Empty;
            result = await ApiManager.GET_SUBSCRIBE(_REQUEST);
            if (!string.IsNullOrEmpty(result))
            {
                _RESPONSE = HelperManger.CastToResponse(result);
                if (string.IsNullOrEmpty(_RESPONSE[0].ERROR_MESSAGE))
                {
                    //List<CONTACT> lstCONTACT = JsonConvert.DeserializeObject<List<CONTACT>>(_RESPONSE[0].JSON);
                    //obsContactData = new ObservableCollection<CONTACT>(lstCONTACT);
                }

                NotifyAllPropertiesChanged();
            }
        }

        private bool _arabicIsToggled = false;
        public bool ArabicIsToggled { get { return _arabicIsToggled; } set { _arabicIsToggled = value; } }
        private bool _englishIsToggled = false;
        public bool EnglishIsToggled { get { return _englishIsToggled; } set { _englishIsToggled = value; } }
        private bool _frenchIsToggled = false;
        public bool FrenchIsToggled { get { return _frenchIsToggled; } set { _frenchIsToggled = value; } }
    }
}