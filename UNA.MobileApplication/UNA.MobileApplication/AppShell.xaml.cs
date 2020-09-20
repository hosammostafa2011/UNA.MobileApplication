using Helper;
using Model.Mobile;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.SharedTransitions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using UNA.MobileApplication.ViewModels;
using UNA.MobileApplication.Views;
using Xamarin.Forms;

namespace UNA.MobileApplication
{
    public partial class AppShell : SharedTransitionShell
    {
        public ObservableCollection<CATEGORY> CATEGORY_DATA { get; set; }

        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(HomePageList), typeof(HomePageList));
            /*Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));*/
            Shell.SetTabBarIsVisible(this, false);
            //BindingContext = _viewModel = new vmAppShell();
            Device.BeginInvokeOnMainThread(async () =>
            {
                await new BaseViewModel().RunSafe(LoadCategory(), true);
            });
        }

        private async Task LoadCategory()
        {
            BaseViewModel obj = new BaseViewModel();
            obj._REQUEST.LANGUAGE = "1";
            obj._REQUEST.USER_TOKEN = "Aa@159357";
            var result = await obj.ApiManager.GET_CATEGORY(obj._REQUEST);
            obj._RESPONSE = HelperManger.CastToResponse(result);

            if (string.IsNullOrEmpty(obj._RESPONSE[0].ERROR_MESSAGE))
            {
                List<CATEGORY> lstCATEGORY = JsonConvert.DeserializeObject<List<CATEGORY>>(obj._RESPONSE[0].JSON);
                foreach (CATEGORY objCATEGORY in lstCATEGORY)
                {
                    ShellSection shell_section = new ShellSection
                    {
                        Title = objCATEGORY.CategoryName,
                        Icon = "tab_about.png",
                    };
                    shell_section.Items.Add(new ShellContent() { Content = new NewsList(objCATEGORY.CategoryID) });
                    lstCategory.Items.Add(shell_section);
                }
            }
        }
    }
}