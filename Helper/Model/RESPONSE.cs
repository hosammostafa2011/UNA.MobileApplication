using Helper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Mobile
{
    public class RESPONSE : CoreModel
    {
        public RESPONSE()
        {

        }

        public RESPONSE(string pERROR_CODE, string pERROR_MESSAGE, bool pVALID_TOKEN, string pJSON)
        {
            this.ERROR_CODE = pERROR_CODE;
            this.ERROR_MESSAGE = pERROR_MESSAGE;
            this.VALID_TOKEN = pVALID_TOKEN;
            this.JSON = pJSON;
        }
        public RESPONSE(string pERROR_CODE, string pERROR_MESSAGE, bool pVALID_TOKEN, string pJSON, string pOUT_PARAMETER)
        {
            this.ERROR_CODE = pERROR_CODE;
            this.ERROR_MESSAGE = pERROR_MESSAGE;
            this.VALID_TOKEN = pVALID_TOKEN;
            this.JSON = pJSON;
            this.OUT_PARAMETER = pOUT_PARAMETER;

        }
        public string JSON { get; set; }
        public bool VALID_TOKEN { get; set; }
        public string DEVELOPER_TOKEN { get; set; }
        public string OUT_PARAMETER { get; set; }
        public string TOKEN { get; set; }
        public string LANGUAGE { get; set; }
    }
}
