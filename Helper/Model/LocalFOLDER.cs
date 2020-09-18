using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Helper.XamarinModel
{
    public class LocalFOLDER: INotifyPropertyChanged
    {
        private string fOLDER_NAME;
        public string FOLDER_NAME
        {
            get { return fOLDER_NAME; }
            set
            {
                fOLDER_NAME = value;
                OnPropertyChanged();
            }
        }
        private string fOLDER_ID;
        public string FOLDER_ID
        {
            get { return fOLDER_ID; }
            set
            {
                fOLDER_ID = value;
                OnPropertyChanged();
            }
        }

        private string mATERIAL_ICON;
        public string MATERIAL_ICON
        {
            get { return mATERIAL_ICON; }
            set
            {
                mATERIAL_ICON = value;
                OnPropertyChanged();
            }
        }


        private bool iS_CHECKED;
        public bool IS_CHECKED
        {
            get { return iS_CHECKED; }
            set
            {
                iS_CHECKED = value;
                OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
