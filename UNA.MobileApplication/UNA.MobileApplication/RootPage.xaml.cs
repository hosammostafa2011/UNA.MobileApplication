using Google.Api.Gax.Rest;
using Plugin.SecureStorage;
using Plugin.SharedTransitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNA.MobileApplication.Interface;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Plugin.SharedTransitions;

namespace UNA.MobileApplication
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RootPage : MasterDetailPage
    {
        public RootPage()
        {
            InitializeComponent();
            if (!CrossSecureStorage.Current.HasKey("Language"))
            {
                CrossSecureStorage.Current.SetValue("Language", "1");
            }
            try
            {
                if (CrossSecureStorage.Current.GetValue("Language").Equals("1"))
                {
                    MasterPage.FlowDirection = FlowDirection.RightToLeft;
                    Detail.FlowDirection = FlowDirection.RightToLeft;
                    DependencyService.Get<IPathManager>().SetLayoutRTL();
                }
                else
                {
                    DependencyService.Get<IPathManager>().SetLayoutLTR();
                }
            }
            catch (Exception)
            {
                CrossSecureStorage.Current.SetValue("Language", "1");
                DependencyService.Get<IPathManager>().SetLayoutRTL();
            }
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as RootPageMasterMenuItem;
            if (item == null)
                return;
            //categoryID, string categoryName, string nationID
            var page = (Page)Activator.CreateInstance(
                item.TargetType, new object[] { item.CategoryID, item.Title, item.NationID }
                );
            page.Title = item.Title;
            
            Detail = new SharedTransitionNavigationPage(page);
            
            //Navigation.PushAsync(new Profile());
            IsPresented = false;
            IconImageSource = "hamburguer_icon";
            MasterPage.ListView.SelectedItem = null;
        }
    }
}