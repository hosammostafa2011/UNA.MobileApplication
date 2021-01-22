using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Helper.Interface;
using Model.Mobile;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Polly;
using Refit;

namespace Helper.Service
{
    public class MobileApiManager : IMobileApiManager
    {
        private IUserDialogs _userDialogs = UserDialogs.Instance;
        private IConnectivity _connectivity = CrossConnectivity.Current;

        //IApiService<IMakeUpApi> makeUpApi;
        private IApiService<IMobileApiManager> makeUpApi;

        public bool IsConnected { get; set; }
        public bool IsReachable { get; set; }
        private Dictionary<int, CancellationTokenSource> runningTasks = new Dictionary<int, CancellationTokenSource>();
        private Dictionary<string, Task<HttpResponseMessage>> taskContainer = new Dictionary<string, Task<HttpResponseMessage>>();

        #region General Setting

        public MobileApiManager(IApiService<IMobileApiManager> _makeUpApi)
        {
            makeUpApi = _makeUpApi;
            IsConnected = _connectivity.IsConnected;
            _connectivity.ConnectivityChanged += OnConnectivityChanged;
        }

        private void OnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            IsConnected = e.IsConnected;

            if (!e.IsConnected)
            {
                // Cancel All Running Task
                var items = runningTasks.ToList();
                foreach (var item in items)
                {
                    item.Value.Cancel();
                    runningTasks.Remove(item.Key);
                }
            }
        }

        #endregion General Setting

        protected async Task<TData> RemoteRequestAsync<TData>(Task<TData> task)
            where TData : HttpResponseMessage,
            new()
        {
            TData data = new TData();

            if (!IsConnected)
            {
                var strngResponse = "There's not a network connection";
                data.StatusCode = HttpStatusCode.BadRequest;
                data.Content = new StringContent(strngResponse);

                _userDialogs.Toast(strngResponse, TimeSpan.FromSeconds(1));
                return data;
            }

            IsReachable = await _connectivity.IsRemoteReachable(Constant.ApiHostName);

            if (!IsReachable)
            {
                var strngResponse = "There's not an internet connection";
                data.StatusCode = HttpStatusCode.BadRequest;
                data.Content = new StringContent(strngResponse);

                _userDialogs.Toast(strngResponse, TimeSpan.FromSeconds(1));
                return data;
            }

            //data.Content = new StringContent(JSON_CONTENT);

            data = await Policy
            .Handle<WebException>()
            .Or<ApiException>()
            .Or<TaskCanceledException>()
            .WaitAndRetryAsync
            (
                retryCount: 1,
                sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
            )
            .ExecuteAsync(async () =>
            {
                var result = await task;

                if (result.StatusCode == HttpStatusCode.Unauthorized)
                {
                    //Logout the user
                }
                runningTasks.Remove(task.Id);

                return result;
            });

            return data;
        }

        #region UNA APP

        public async Task<string> GET_LATEST_NEWS(REQUEST request)
        {
            var apiresponse = RestService.For<IMobileApiManager>(Constant.ApiUrl);
            var response = await apiresponse.GET_LATEST_NEWS(request);
            return response;
        }

        public async Task<string> GET_CATEGORY(REQUEST request)
        {
            try
            {
                var apiresponse = RestService.For<IMobileApiManager>(Constant.ApiUrl);
                var response = await apiresponse.GET_CATEGORY(request);
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<string> GET_NEWS_BY_CATEGORY(REQUEST request)
        {
            var apiresponse = RestService.For<IMobileApiManager>(Constant.ApiUrl);
            var response = await apiresponse.GET_NEWS_BY_CATEGORY(request);
            return response;
        }

        public async Task<string> GET_NEWS_DETAIL(REQUEST request)
        {
            var apiresponse = RestService.For<IMobileApiManager>(Constant.ApiUrl);
            var response = await apiresponse.GET_NEWS_DETAIL(request);
            return response;
        }

        public async Task<string> GET_TOP_NEWS(REQUEST request)
        {
            try
            {
                var apiresponse = RestService.For<IMobileApiManager>(Constant.ApiUrl);
                var response = await apiresponse.GET_TOP_NEWS(request);
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> GET_REPORT(REQUEST request)
        {
            var apiresponse = RestService.For<IMobileApiManager>(Constant.ApiUrl);
            var response = await apiresponse.GET_REPORT(request);
            return response;
        }

        public async Task<string> GET_REPORT_DETAIL(REQUEST request)
        {
            var apiresponse = RestService.For<IMobileApiManager>(Constant.ApiUrl);
            var response = await apiresponse.GET_REPORT_DETAIL(request);
            return response;
        }

        public async Task<string> GET_PHOTO_ALBUM(REQUEST request)
        {
            var apiresponse = RestService.For<IMobileApiManager>(Constant.ApiUrl);
            var response = await apiresponse.GET_PHOTO_ALBUM(request);
            return response;
        }

        public async Task<string> GET_PHOTO_ALBUM_DETAIL(REQUEST request)
        {
            var apiresponse = RestService.For<IMobileApiManager>(Constant.ApiUrl);
            var response = await apiresponse.GET_PHOTO_ALBUM_DETAIL(request);
            return response;
        }

        public async Task<string> GET_VIDEO(REQUEST request)
        {
            var apiresponse = RestService.For<IMobileApiManager>(Constant.ApiUrl);
            var response = await apiresponse.GET_VIDEO(request);
            return response;
        }

        public async Task<string> GET_FAVOURITE(REQUEST request)
        {
            var apiresponse = RestService.For<IMobileApiManager>(Constant.ApiUrl);
            var response = await apiresponse.GET_FAVOURITE(request);
            return response;
        }

        public async Task<string> GET_NEWS_BY_NATION(REQUEST request)
        {
            var apiresponse = RestService.For<IMobileApiManager>(Constant.ApiUrl);
            var response = await apiresponse.GET_NEWS_BY_NATION(request);
            return response;
        }

        public async Task<string> GET_NATION(REQUEST request)
        {
            try
            {
                var apiresponse = RestService.For<IMobileApiManager>(Constant.ApiUrl);
                var response = await apiresponse.GET_NATION(request);
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<string> GET_CONTACT(REQUEST request)
        {
            var apiresponse = RestService.For<IMobileApiManager>(Constant.ApiUrl);
            var response = await apiresponse.GET_CONTACT(request);
            return response;
        }

        public async Task<string> SET_FCM_TOKEN(REQUEST request)
        {
            var apiresponse = RestService.For<IMobileApiManager>(Constant.ApiUrl);
            var response = await apiresponse.SET_FCM_TOKEN(request);
            return response;
        }

        public async Task<string> GET_SUBSCRIBE(REQUEST request)
        {
            var apiresponse = RestService.For<IMobileApiManager>(Constant.ApiUrl);
            var response = await apiresponse.GET_SUBSCRIBE(request);
            return response;
        }

        public async Task<string> SET_SUBSCRIBE(REQUEST request)
        {
            var apiresponse = RestService.For<IMobileApiManager>(Constant.ApiUrl);
            var response = await apiresponse.SET_SUBSCRIBE(request);
            return response;
        }
        public async Task<string> GET_SEARCH_NEWS(REQUEST request)
        {
            var apiresponse = RestService.For<IMobileApiManager>(Constant.ApiUrl);
            var response = await apiresponse.GET_SEARCH_NEWS(request);
            return response;
        }

        #endregion UNA APP
    }
}