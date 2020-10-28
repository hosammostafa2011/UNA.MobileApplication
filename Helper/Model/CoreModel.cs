using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Helper.Model
{
    public class CoreModel : INotifyPropertyChanged
    {
        public string ERROR_CODE { get; set; }
        public string ERROR_MESSAGE { get; set; }
        public string Language_ID { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}