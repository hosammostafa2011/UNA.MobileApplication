using Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UNA.MobileApplication.ViewModels;
using UNA.MobileApplication.Views;
using Xamarin.Forms;

namespace UNA.MobileApplication
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(HomePageList), typeof(HomePageList));
            /*Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));*/
            Shell.SetTabBarIsVisible(this, false);
            Device.BeginInvokeOnMainThread(async () =>
            {
                await new BaseViewModel().RunSafe(LoadCategory(), true);
            });
            for (int i = 0; i < 5; i++)
            {
                ShellSection shell_section = new ShellSection
                {
                    Title = "Category " + i.ToString(),
                    Icon = "tab_about.png"
                };
                shell_section.Items.Add(new ShellContent() { Content = new NewsList() });
                //myshell.Items.Add(shell_section);
                lstCategory.Items.Add(shell_section);
            }
        }

        private async Task LoadCategory()
        {
            BaseViewModel obj = new BaseViewModel();
            //obj._REQUEST.JSON = JsonConvert.SerializeObject("{}");
            var result = await obj.ApiManager.GET_CATEGORY(obj._REQUEST);
            obj._RESPONSE = HelperManger.CastToResponse(result);
        }
    }
}