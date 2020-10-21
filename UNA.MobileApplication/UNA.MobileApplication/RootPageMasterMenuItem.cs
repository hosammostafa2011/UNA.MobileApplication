using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNA.MobileApplication
{
    public class RootPageMasterMenuItem
    {
        public RootPageMasterMenuItem(string _TITLE, string _ICON, Type _TYPE)
        {
            this.TargetType = _TYPE;
            this.Title = _TITLE;
            this.Icon = _ICON;
        }

        public string Title { get; set; }
        public string Icon { get; set; }

        public Type TargetType { get; set; }
    }
}