using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNA.MobileApplication
{
    public class RootPageMasterMenuItem
    {
        public RootPageMasterMenuItem(string _TITLE, string _ICON, string _CATEGORYID, Type _TYPE)
        {
            this.TargetType = _TYPE;
            this.Title = _TITLE;
            this.Icon = _ICON;
            this.CategoryID = _CATEGORYID;
            // this.NationID = _NATIONID;
        }

        public string NationID { get; set; }

        public string Title { get; set; }
        public string Icon { get; set; }
        public string CategoryID { get; set; }

        public Type TargetType { get; set; }
    }
}