using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace UNA.MobileApplication.ViewModels
{
    public class SettingViewModel : BaseViewModel
    {
        public SettingViewModel()
        {
        }
        bool _arabicIsToggled = false;
        public bool ArabicIsToggled { get { return _arabicIsToggled; } set { _arabicIsToggled = value; } }
        bool _englishIsToggled = false;
        public bool EnglishIsToggled { get { return _englishIsToggled; } set { _englishIsToggled = value; } }
        bool _frenchIsToggled = false;
        public bool FrenchIsToggled { get { return _frenchIsToggled; } set { _frenchIsToggled = value; } }
    }
}