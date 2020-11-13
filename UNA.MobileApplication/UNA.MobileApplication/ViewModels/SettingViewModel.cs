using FirebaseAdmin.Messaging;
using Helper;
using Helper.Model;
using Newtonsoft.Json;
using Plugin.FirebasePushNotification;
using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace UNA.MobileApplication.ViewModels
{
    public class SettingViewModel : BaseViewModel
    {
        public ICommand SaveCommand { get; set; }

        public SettingViewModel()
        {
            Device.BeginInvokeOnMainThread(async () => await RunSafe(GetNotificationAsync(), true));
            SaveCommand = new Command(async () => await RunSafe(Save()));
        }

        private async Task Save()
        {
            MessagingCenter.Send<string, string>("MyApp", "TokenChanges", CrossFirebasePushNotification.Current.Token);

            if (ArabicIsToggled)
                _REQUEST.LANGUAGE = "1";
            else if (EnglishIsToggled)
                _REQUEST.LANGUAGE = "2";
            else if (FrenchIsToggled)
                _REQUEST.LANGUAGE = "3";
            else
                _REQUEST.LANGUAGE = string.Empty;
            
            _REQUEST.DEVICE_PLATFORM = DeviceInfo.Platform.ToString().ToLower();
            _REQUEST.USER_TOKEN = "Aa159357";
            _REQUEST.FCM_TOKEN = CrossFirebasePushNotification.Current.Token;
            _REQUEST.JSON = string.Empty;
            string result = await ApiManager.SET_SUBSCRIBE(_REQUEST);
            if (!string.IsNullOrEmpty(result))
            {
                _RESPONSE = HelperManger.CastToResponse(result);
                if (string.IsNullOrEmpty(_RESPONSE[0].ERROR_MESSAGE))
                {
                    switch (_REQUEST.LANGUAGE)
                    {
                        case "1":
                            CrossFirebasePushNotification.Current.Subscribe("1");
                            CrossFirebasePushNotification.Current.Unsubscribe(new string[] { "2", "3" });
                            break;

                        case "2":
                            CrossFirebasePushNotification.Current.Subscribe("2");
                            CrossFirebasePushNotification.Current.Unsubscribe(new string[] { "1", "3" });
                            break;

                        case "3":
                            CrossFirebasePushNotification.Current.Subscribe("3");
                            CrossFirebasePushNotification.Current.Unsubscribe(new string[] { "1", "2" });
                            break;

                        default:
                            CrossFirebasePushNotification.Current.Unsubscribe(new string[] { "1", "2", "3" });
                            break;
                    }
                }
                //NotifyAllPropertiesChanged();
            }
        }

        private async Task GetNotificationAsync()
        {
            //MessagingCenter.Send<string, string>("MyApp", "Subscribe", "1");

            string FCM = string.Empty;
            if (CrossSecureStorage.Current.HasKey("FCMToken"))
            {
                FCM = CrossSecureStorage.Current.GetValue("FCMToken");
                try
                {
                    _REQUEST.LANGUAGE = CrossSecureStorage.Current.GetValue("Language");
                }
                catch (Exception)
                {
                    _REQUEST.LANGUAGE = "1";
                }
                _REQUEST.USER_TOKEN = "Aa@159357";
                _REQUEST.FCM_TOKEN = FCM;
                _REQUEST.DEVICE_PLATFORM = DeviceInfo.Platform.ToString().ToLower();
                var result = await ApiManager.SET_FCM_TOKEN(_REQUEST);

                // no value
                _REQUEST.USER_TOKEN = "Aa159357";
                _REQUEST.FCM_TOKEN = FCM;
                _REQUEST.JSON = string.Empty;
                result = await ApiManager.GET_SUBSCRIBE(_REQUEST);
                if (!string.IsNullOrEmpty(result))
                {
                    _RESPONSE = HelperManger.CastToResponse(result);
                    if (string.IsNullOrEmpty(_RESPONSE[0].ERROR_MESSAGE))
                    {
                        List<CoreModel> lstCoreModel = JsonConvert.DeserializeObject<List<CoreModel>>(_RESPONSE[0].JSON);

                        switch (lstCoreModel[0].Language_ID)
                        {
                            case "1":
                                ArabicIsToggled = true;
                                EnglishIsToggled = FrenchIsToggled = false;
                                break;

                            case "2":
                                EnglishIsToggled = true;
                                ArabicIsToggled = FrenchIsToggled = false;
                                break;

                            case "3":
                                FrenchIsToggled = true;
                                ArabicIsToggled = EnglishIsToggled = false;
                                break;

                            default:
                                ArabicIsToggled = EnglishIsToggled = FrenchIsToggled = false;
                                break;
                        }
                    }
                    NotifyAllPropertiesChanged();
                }
            }
            else
            {
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