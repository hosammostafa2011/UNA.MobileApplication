using System;
using System.Collections.Generic;
using System.Text;
using UNA.MobileApplication.Views.DataTemplate;
using Xamarin.Forms;

namespace UNA.MobileApplication.Util
{
    class NewsDataTemplateSelector : DataTemplateSelector
    {
        public NewsDataTemplateSelector()
        {
            // Retain instances
            this.templateOne = new DataTemplate(typeof(OneNewsView));
            this.templateTwo = new DataTemplate(typeof(TwoColumnView));
            this.templateThree = new DataTemplate(typeof(ListNewsView));
        }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            //chhose the template
            /*if (item is double)
                return this.templateOne;
            return this.templateTwo;*/
            return templateThree;
        }

        public DataTemplate templateOne;
        public DataTemplate templateTwo;
        public DataTemplate templateThree;
    }
}
