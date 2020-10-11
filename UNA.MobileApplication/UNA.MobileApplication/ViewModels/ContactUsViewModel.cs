using Helper;
using Helper.Model;
using Newtonsoft.Json;
using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace UNA.MobileApplication.ViewModels
{
    public class ContactUsViewModel : BaseViewModel
    {
        private ObservableCollection<CONTACT> CONTACT_DATA;
        public ObservableCollection<CONTACT> obsContactData { get; set; } = new ObservableCollection<CONTACT>();
        public string TITLE_01 { get; set; }
        public string TITLE_01_VAL { get; set; }
        public string TITLE_02 { get; set; }
        public string TITLE_02_VAL { get; set; }
        public string TITLE_03 { get; set; }
        public string TITLE_03_VAL { get; set; }
        public string TITLE_04 { get; set; }
        public string TITLE_04_VAL { get; set; }
        public string TITLE_05 { get; set; }
        public string TITLE_05_VAL { get; set; }
        public string TITLE_06 { get; set; }
        public string TITLE_06_VAL { get; set; }
        public string TITLE_07 { get; set; }
        public string TITLE_07_VAL { get; set; }

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
            result = await ApiManager.GET_CONTACT(_REQUEST);
            if (!string.IsNullOrEmpty(result))
            {
                _RESPONSE = HelperManger.CastToResponse(result);
                if (string.IsNullOrEmpty(_RESPONSE[0].ERROR_MESSAGE))
                {
                    List<CONTACT> lstCONTACT = JsonConvert.DeserializeObject<List<CONTACT>>(_RESPONSE[0].JSON);
                    obsContactData = new ObservableCollection<CONTACT>(lstCONTACT);
                }
                foreach (CONTACT contact in obsContactData)
                {
                    switch (contact.order_id)
                    {
                        case "1":
                            TITLE_01 = contact.contact_title;
                            TITLE_01_VAL = contact.email_val;
                            break;
                        case "2":
                            TITLE_02 = contact.contact_title;
                            TITLE_02_VAL = contact.email_val;
                            break;
                        case "3":
                            TITLE_03 = contact.contact_title;
                            TITLE_03_VAL = contact.email_val;
                            break;
                        case "4":
                            TITLE_04 = contact.contact_title;
                            TITLE_04_VAL = contact.email_val;
                            break;
                        case "5":
                            TITLE_05 = contact.contact_title;
                            TITLE_05_VAL = contact.email_val;
                            break;
                        case "6":
                            TITLE_06 = contact.contact_title;
                            TITLE_06_VAL = contact.email_val;
                            break;
                        case "7":
                            TITLE_07 = contact.contact_title;
                            TITLE_07_VAL = contact.email_val;
                            break;
                    }
                }
                NotifyAllPropertiesChanged();
            }
        }
    }
}