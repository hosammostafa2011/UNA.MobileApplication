using FirebaseAdmin.Messaging;
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
using Xamarin.Essentials;
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
            try
            {
                if (CrossSecureStorage.Current.GetValue("Language").Equals("1"))
                    myshell.FlowDirection = FlowDirection.RightToLeft;
                else
                    myshell.FlowDirection = FlowDirection.LeftToRight;
            }
            catch (Exception)
            {
                myshell.FlowDirection = FlowDirection.RightToLeft;
                CrossSecureStorage.Current.SetValue("Language", "1");
            }
            Shell.SetTabBarIsVisible(this, false);
            Device.BeginInvokeOnMainThread(async () =>
            {
                await new BaseViewModel().RunSafe(LoadCategory(), true);
            });
            RegiesterMessageCenter();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private void RegiesterMessageCenter()
        {
            MessagingCenter.Subscribe<string, string>("MyApp", "TokenChanges", async (sender, arg) =>
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
                obj._REQUEST.FCM_TOKEN = arg.ToString();
                obj._REQUEST.DEVICE_PLATFORM = DeviceInfo.Platform.ToString().ToLower();
                var result = await obj.ApiManager.SET_FCM_TOKEN(obj._REQUEST);
            });
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
                        Icon = string.IsNullOrEmpty(objCATEGORY.App_Menu_Ico) ? "world.png" : objCATEGORY.App_Menu_Ico,
                    };
                    if (objCATEGORY.Category_ID == "0")
                        shell_section.Items.Add(new ShellContent() { Content = new HomePageList(string.Empty, string.Empty, string.Empty) });
                    else if (objCATEGORY.Category_ID == "1000")
                        shell_section.Items.Add(new ShellContent() { Content = new ReportPage(string.Empty, string.Empty, string.Empty) });
                    else if (objCATEGORY.Category_ID == "1100")
                        shell_section.Items.Add(new ShellContent() { Content = new PhotoAlbum(string.Empty, string.Empty, string.Empty) });
                    else if (objCATEGORY.Category_ID == "1200")
                        shell_section.Items.Add(new ShellContent() { Content = new VideoAlbum(string.Empty, string.Empty, string.Empty) });
                    else if (objCATEGORY.Category_ID == "1300")
                        shell_section.Items.Add(new ShellContent() { Content = new NationPage(string.Empty, string.Empty, string.Empty) });
                    else if (objCATEGORY.Category_ID == "7000")
                        shell_section.Items.Add(new ShellContent() { Content = new FavouritePage("7000", string.Empty, string.Empty) });
                    else if (objCATEGORY.Category_ID == "1400")
                        shell_section.Items.Add(new ShellContent() { Content = new MostReadPage("1400", string.Empty, string.Empty) });
                    else if (objCATEGORY.Category_ID == "1600")
                        shell_section.Items.Add(new ShellContent() { Content = new ContactUs(string.Empty, string.Empty, string.Empty) });
                    else if (objCATEGORY.Category_ID == "1500")
                        shell_section.Items.Add(new ShellContent() { Content = new SettingViewPage(string.Empty, string.Empty, string.Empty) });
                    else
                        shell_section.Items.Add(new ShellContent() { Content = new NewsList(objCATEGORY.Category_ID, objCATEGORY.CategoryName, string.Empty) });
                    lstCategory.Items.Add(shell_section);
                }
                try
                {
                    VersionTracking.Track();
                    var currentVersion = VersionTracking.CurrentVersion;
                    if (currentVersion != lstCATEGORY[0].CurrentVersion)
                    {
                        if (obj._REQUEST.LANGUAGE == "1")
                            HelperManger.ShowToast("توجد نسخة حديثة من التطبيق - قم بتحديث التطبيق");
                        else if (obj._REQUEST.LANGUAGE == "2")
                            HelperManger.ShowToast("There is a fresh version of the application - update the application.");
                        else
                            HelperManger.ShowToast("Il existe une nouvelle version de l'application-mettez à jour l'application.");
                    }
                }
                catch (System.Exception)
                {
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

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            /*var dialog = new ActionView();//popup
            await PopupNavigation.Instance.PushAsync(dialog);*/
        }
    }
}