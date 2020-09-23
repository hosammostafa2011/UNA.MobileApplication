using Xamarin.Forms;

namespace UNA.MobileApplication.Util
{
    public class NewsDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TemplateOne { get; set; }
        public DataTemplate TemplateTwo { get; set; }
        public DataTemplate TemplateThree { get; set; }

        public DataTemplate InvalidTemplate { get; set; }
        int index = 0;
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
             string RowNumber=((NEWS)item).RowNumber;
            if (RowNumber == "1")
                return TemplateOne;
            else if(RowNumber=="2")
                return TemplateTwo;
            else
                return TemplateThree;
        }
        public NewsDataTemplateSelector()
        {
            // Retain instances
            /*this.templateOne = new DataTemplate(typeof(OneNewsView));
            this.templateTwo = new DataTemplate(typeof(TwoColumnView));
            this.templateThree = new DataTemplate(typeof(ListNewsView));*/
        }
        //protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        //{
        //    //chhose the template
        //    /*if (item is double)
        //        return this.templateOne;
        //    return this.templateTwo;*/
        //    return templateThree;
        //}
    }
}
