﻿using Helper;
using Newtonsoft.Json;
using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using UNA.MobileApplication.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace UNA.MobileApplication.ViewModels
{
    public class RootPageMasterViewModel : BaseViewModel

    {
        public Command LoadNewsCommand { get; set; }

        public ObservableCollection<RootPageMasterMenuItem> MenuItems { get; set; } =
            new ObservableCollection<RootPageMasterMenuItem>();

        public RootPageMasterViewModel()
        {
            Device.BeginInvokeOnMainThread(async () => await RunSafe(LoadCategory(), true));
        }

        private async Task LoadCategory()
        {
            try
            {
                _REQUEST.LANGUAGE = CrossSecureStorage.Current.GetValue("Language");
            }
            catch (Exception)
            {
                _REQUEST.LANGUAGE = "1";
            }
            _REQUEST.USER_TOKEN = "Aa@159357";
            string icon = string.Empty;
            var result = await ApiManager.GET_CATEGORY(_REQUEST);
            _RESPONSE = HelperManger.CastToResponse(result);
            if (string.IsNullOrEmpty(_RESPONSE[0].ERROR_MESSAGE))
            {
                List<CATEGORY> lstCATEGORY = JsonConvert.DeserializeObject<List<CATEGORY>>(_RESPONSE[0].JSON);
                MenuItems = new ObservableCollection<RootPageMasterMenuItem>();
                foreach (CATEGORY objCATEGORY in lstCATEGORY)
                {
                    try
                    {
                        icon = string.IsNullOrEmpty(objCATEGORY.App_Menu_Ico) ? "world.png" : objCATEGORY.App_Menu_Ico;
                        if (objCATEGORY.Category_ID == "0")
                            MenuItems.Add(new RootPageMasterMenuItem(objCATEGORY.CategoryName, icon, objCATEGORY.Category_ID, typeof(HomePageList)));
                        else if (objCATEGORY.Category_ID == "1000")
                            MenuItems.Add(new RootPageMasterMenuItem(objCATEGORY.CategoryName, icon, objCATEGORY.Category_ID, typeof(ReportPage)));
                        else if (objCATEGORY.Category_ID == "1100")
                            MenuItems.Add(new RootPageMasterMenuItem(objCATEGORY.CategoryName, icon, objCATEGORY.Category_ID, typeof(PhotoAlbum)));
                        else if (objCATEGORY.Category_ID == "1200")
                            MenuItems.Add(new RootPageMasterMenuItem(objCATEGORY.CategoryName, icon, objCATEGORY.Category_ID, typeof(VideoAlbum)));
                        else if (objCATEGORY.Category_ID == "7000")
                            MenuItems.Add(new RootPageMasterMenuItem(objCATEGORY.CategoryName, icon, objCATEGORY.Category_ID, typeof(FavouritePage)));
                        else if (objCATEGORY.Category_ID == "1400")
                            MenuItems.Add(new RootPageMasterMenuItem(objCATEGORY.CategoryName, icon, objCATEGORY.Category_ID, typeof(MostReadPage)));
                        else if (objCATEGORY.Category_ID == "1600")
                            MenuItems.Add(new RootPageMasterMenuItem(objCATEGORY.CategoryName, icon, objCATEGORY.Category_ID, typeof(ContactUs)));
                        else
                            MenuItems.Add(new RootPageMasterMenuItem(objCATEGORY.CategoryName, icon, objCATEGORY.Category_ID, typeof(NewsList)));
                    }
                    catch (Exception ex)
                    {
                    }
                    //ShellSection shell_section = new ShellSectionf
                    //{
                    //    Title = objCATEGORY.CategoryName,
                    //    Icon = string.IsNullOrEmpty(objCATEGORY.App_Menu_Ico) ? "world.png" : objCATEGORY.App_Menu_Ico,
                    //};
                    //if (objCATEGORY.Category_ID == "0")
                    //    shell_section.Items.Add(new ShellContent() { Content = new HomePageList() });
                    //else if (objCATEGORY.Category_ID == "1000")
                    //    shell_section.Items.Add(new ShellContent() { Content = new ReportPage() });
                    //else if (objCATEGORY.Category_ID == "1100")
                    //    shell_section.Items.Add(new ShellContent() { Content = new PhotoAlbum() });
                    //else if (objCATEGORY.Category_ID == "1200")
                    //    shell_section.Items.Add(new ShellContent() { Content = new VideoAlbum() });
                    //else if (objCATEGORY.Category_ID == "1300")
                    //    shell_section.Items.Add(new ShellContent() { Content = new NationPage() });
                    //else if (objCATEGORY.Category_ID == "7000")
                    //    shell_section.Items.Add(new ShellContent() { Content = new FavouritePage("7000") });
                    //else if (objCATEGORY.Category_ID == "1400")
                    //    shell_section.Items.Add(new ShellContent() { Content = new MostReadPage("1400") });
                    //else if (objCATEGORY.Category_ID == "1600")
                    //    shell_section.Items.Add(new ShellContent() { Content = new ContactUs() });
                    //else
                    //    shell_section.Items.Add(new ShellContent() { Content = new NewsList(objCATEGORY.Category_ID, objCATEGORY.CategoryName, string.Empty) });
                    ////lstCategory.Items.Add(shell_section);
                }
                try
                {
                    VersionTracking.Track();
                    var currentVersion = VersionTracking.CurrentVersion;
                    if (currentVersion != lstCATEGORY[0].CurrentVersion)
                    {
                        if (_REQUEST.LANGUAGE == "1")
                            HelperManger.ShowToast("توجد نسخة حديثة من التطبيق - قم بتحديث التطبيق");
                        else if (_REQUEST.LANGUAGE == "2")
                            HelperManger.ShowToast("There is a fresh version of the application - update the application.");
                        else
                            HelperManger.ShowToast("Il existe une nouvelle version de l'application-mettez à jour l'application.");
                    }
                }
                catch (System.Exception)
                {
                }
                NotifyPropertyChanged(nameof(MenuItems));
            }
        }
    }
}