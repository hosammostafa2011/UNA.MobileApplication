using System.ComponentModel;
using Xamarin.Forms;
using UNA.MobileApplication.ViewModels;

namespace UNA.MobileApplication.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}