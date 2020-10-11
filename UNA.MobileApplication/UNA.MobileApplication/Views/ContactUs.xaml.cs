using UNA.MobileApplication.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UNA.MobileApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactUs : ContentPage
    {
        private readonly  ContactUsViewModel contactUsViewModel = null;
        public ContactUs()
        {
            InitializeComponent();
            BindingContext = contactUsViewModel = new ContactUsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Shell.SetNavBarIsVisible(this, true);
            if (contactUsViewModel.obsContactData.Count <= 0)
                contactUsViewModel.LoadContactDataCommand.Execute(null);
        }
    }
}