using System.Collections.Generic;
using System.ComponentModel;

namespace UNA.MobileApplication.Models
{
    public class Tab : INotifyPropertyChanged
    {
        public string Id { get; set; }
        public bool Selected { get; set; }
        public string Title { get; set; }
        public List<Place> Items { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}