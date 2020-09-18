using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UNA.MobileApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsList : ContentPage
    {
        public string CategoryId { get; set; }
        public NewsList(string categoryID)
        {
            InitializeComponent();
            CategoryId = categoryID;
            //load data by cat id
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}