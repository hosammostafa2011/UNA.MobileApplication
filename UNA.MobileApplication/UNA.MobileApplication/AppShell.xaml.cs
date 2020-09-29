using Helper;
using Model.Mobile;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.SecureStorage;
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
            if (!CrossSecureStorage.Current.HasKey("Language"))
            {
                CrossSecureStorage.Current.SetValue("Language", "1");
            }

            if (CrossSecureStorage.Current.GetValue("Language").Equals("1"))
                myshell.FlowDirection = FlowDirection.RightToLeft;
            else
                myshell.FlowDirection = FlowDirection.LeftToRight;

            /*Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));*/
            Shell.SetTabBarIsVisible(this, false);
            //BindingContext = _viewModel = new vmAppShell();
            Device.BeginInvokeOnMainThread(async () =>
            {
                await new BaseViewModel().RunSafe(LoadCategory(), true);
            });
            //Routing.RegisterRoute(nameof(HomePageList), typeof(HomePageList));
            //Routing.RegisterRoute(nameof(ReportPage), typeof(ReportPage));
        }

        private async Task LoadCategory()
        {
            BaseViewModel obj = new BaseViewModel();
            try
            {
                obj._REQUEST.LANGUAGE = CrossSecureStorage.Current.GetValue("Language");
            }
            catch (Exception)
            {
                obj._REQUEST.LANGUAGE = "1";
            }
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
                    if (objCATEGORY.Category_ID == "0")
                        shell_section.Items.Add(new ShellContent() { Content = new HomePageList() });
                    else if (objCATEGORY.Category_ID == "1000")
                        shell_section.Items.Add(new ShellContent() { Content = new ReportPage() });
                    else if (objCATEGORY.Category_ID == "1100")
                        shell_section.Items.Add(new ShellContent() { Content = new PhotoAlbum() });
                    else if (objCATEGORY.Category_ID == "1200")
                        shell_section.Items.Add(new ShellContent() { Content = new VideoAlbum() });
                    else if (objCATEGORY.Category_ID == "7000")
                        shell_section.Items.Add(new ShellContent() { Content = new FavouritePage("7000") });
                    else
                        shell_section.Items.Add(new ShellContent() { Content = new NewsList(objCATEGORY.Category_ID) });
                    lstCategory.Items.Add(shell_section);
                }
            }
            //-------
            //ShellSection langShell = new ShellSection
            //{
            //    Title = "عربي",
            //    Icon = "tab_about.png",
            //};
            //langShell.Items.Add(new ShellContent() { Content = new HomePageList() });
            //lstLang.Items.Add(langShell);

            //ShellSection langShell2 = new ShellSection
            //{
            //    Title = "English",
            //    Icon = "tab_about.png",
            //};
            //langShell2.Items.Add(new ShellContent() { Content = new HomePageList() });
            //lstLang.Items.Add(langShell2);

            //ShellSection langShell3 = new ShellSection
            //{
            //    Title = "French",
            //    Icon = "tab_about.png",
            //};
            //langShell3.Items.Add(new ShellContent() { Content = new HomePageList() });
            //lstLang.Items.Add(langShell3);
        }

        private void Arabic_Tapped(object sender, EventArgs e)
        {
            CrossSecureStorage.Current.SetValue("Language", "1");
            HelperManger.ShowToast("تم تغيير لغة التطبيق إلى العربية");
            myshell.FlowDirection = FlowDirection.RightToLeft;
            (Application.Current).MainPage = new AppShell();
        }

        private void English_Tapped(object sender, EventArgs e)
        {
            CrossSecureStorage.Current.SetValue("Language", "2");
            HelperManger.ShowToast("The language of the application has been changed to English");
            //myshell.FlowDirection = FlowDirection.LeftToRight;
            (Application.Current).MainPage = new AppShell();
        }

        private void French_Tapped(object sender, EventArgs e)
        {
            CrossSecureStorage.Current.SetValue("Language", "3");
            HelperManger.ShowToast("La langue de l’application a été modifiée pour Français");
            //myshell.FlowDirection = FlowDirection.LeftToRight;
            (Application.Current).MainPage = new AppShell();
        }
    }
}