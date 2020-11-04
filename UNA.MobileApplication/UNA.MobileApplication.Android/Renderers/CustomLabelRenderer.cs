using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using UNA.MobileApplication.Controls;
using UNA.MobileApplication.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomLabel), typeof(CustomLabelRenderer))]

namespace UNA.MobileApplication.Droid.Renderers
{
    public class CustomLabelRenderer : LabelRenderer
    {
        protected CustomLabel CustomLabel { get; private set; }

        public CustomLabelRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                this.CustomLabel = (CustomLabel)this.Element;
            }
            //*double lineSpacing = this.CustomLabel.LineHeight;
            double lineSpacing = 0.8;
            if (lineSpacing > 0)
            {
                this.Control.SetLineSpacing(1f, (float)lineSpacing);
                this.UpdateLayout();
            }
        }
    }
}