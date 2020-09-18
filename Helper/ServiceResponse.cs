

using Model.Mobile;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class ServiceResponse
    {
        private static HttpClientHandler clientHandler = new HttpClientHandler();
        public async Task<ObservableCollection<RESPONSE>>  Get(string pURL)
        {
            using (HttpClient client = new HttpClient(clientHandler, false))
            {
                string ResponseResult = await client.GetStringAsync(pURL);
                ObservableCollection<RESPONSE> _RESPONSE = JsonConvert.DeserializeObject<ObservableCollection<RESPONSE>>(ResponseResult);
                return _RESPONSE;
            }
        }
    }
}
