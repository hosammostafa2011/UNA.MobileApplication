using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Helper.XamarinModel
{
    public class MenuEntry
    {
        public MenuEntry()
        {
            PAGE_TYPE = typeof(MenuEntry);
        }
        public string NAME { get; set; }
        public string ICON { get; set; }
        public string BADGE { get; set; }

        public bool USE_TRANSPARENT_NAVBAR { get; set; }
        public Type PAGE_TYPE { get; set; }
        public Func<Page> CREATE_PAGE { get; set; }
        public Type NAVIGATION_PAGE_TYPE { get; set; }
        public bool IS_MODAL { get; set; }        
        public int MODULE_ID { get; set; }
        public int MENU_ID { get; set; }
        public string ADDITIONAL_PARAMETER { get; set; }
    }
}
