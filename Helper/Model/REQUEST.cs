using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Mobile
{
    public class REQUEST
    {
        public REQUEST(string user_token, string language)
        { 
            this.USER_TOKEN = user_token;
            this.LANGUAGE = language;
        }
        public REQUEST(string user_token, string developer_token, string language,
            string package_name,
            string device_id, string device_name,
            string device_type, string device_platform, string device_version, string device_idiom,
            string device_model, string device_manufacturer,
            string fcm_token)
        {
            this.DEVELOPER_TOKEN = developer_token;
            this.LANGUAGE = language;
            this.DEVICE_ID = device_id;
            this.DEVICE_NAME = device_name;
            this.DEVICE_TYPE = device_type;
            this.DEVICE_PLATFORM = device_platform;
            this.DEVICE_VERSION = device_version;
            this.DEVICE_IDIOM = device_idiom;
            this.DEVICE_MODEL = device_model;
            this.DEVICE_MANUFACTURER = device_manufacturer;
            this.PACKAGE_NAME = package_name;
            this.USER_TOKEN = user_token;
            this.FCM_TOKEN = fcm_token;

        }

        public REQUEST()
        {
        }

        public string JSON { get; set; }
        public string DEVELOPER_TOKEN { get; set; }
        public string USER_TOKEN { get; set; }
        public string FCM_TOKEN { get; set; }
        public string DEVICE_ID { get; set; }
        public string DEVICE_NAME { get; set; }
        public string DEVICE_TYPE { get; set; }
        public string DEVICE_PLATFORM { get; set; }
        public string DEVICE_VERSION { get; set; }
        public string DEVICE_IDIOM { get; set; }
        public string DEVICE_MODEL { get; set; }
        public string DEVICE_MANUFACTURER { get; set; }
        public string LANGUAGE { get; set; }
        public string PACKAGE_NAME { get; set; }
        public string ROW_COUNT { get; set; }
        public string PAGE_NUMBER { get; set; }
    }
}
