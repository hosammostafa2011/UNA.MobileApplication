using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using UNA.MobileApplication.Controls;
using UNA.MobileApplication.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomLabel), typeof(CustomLabelRenderer))]

namespace UNA.MobileApplication.iOS.Renderers
{
    public class CustomLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            // Make sure control is not null
            var label = (CustomLabel)Element;
            if (label == null || Control == null)
            {
                return;
            }
            if( label.Direction.Equals("RTL"))
            {
                Control.TextAlignment = UITextAlignment.Right;
            }
            else
            {
                Control.TextAlignment = UITextAlignment.Left;
            }
            

            //var labelString = new NSMutableAttributedString(label.Text);
            //var paragraphStyle = new NSMutableParagraphStyle
            //{
            //    LineSpacing = (nfloat)label.LineHeight,
            //    //Alignment = UITextAlignment.Right
            //    //Alignment = UITextAlignment.Justified
            //};
            //var style = UIStringAttributeKey.ParagraphStyle;
            //var range = new NSRange(0, labelString.Length);

            //labelString.AddAttribute(style, paragraphStyle, range);
            //Control.TextAlignment = UITextAlignment.Justified;
            //Control.AttributedText = labelString;
        }
    }
}