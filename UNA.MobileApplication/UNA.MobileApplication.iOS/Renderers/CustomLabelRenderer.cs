using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

using Foundation;
using Plugin.SecureStorage;
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
            string USER_LANGUAGE = "1";
            try
            {
                USER_LANGUAGE = CrossSecureStorage.Current.GetValue("Language");
            }
            catch (Exception)
            {
                USER_LANGUAGE = "1";
            }
            switch (label.Direction)
            {
                case "Start":
                    Control.TextAlignment = USER_LANGUAGE.Equals("1") ? UITextAlignment.Right : UITextAlignment.Left;
                    break;

                case "End":
                    Control.TextAlignment = USER_LANGUAGE.Equals("1") ? UITextAlignment.Left : UITextAlignment.Right;

                    break;

                case "Center":
                    Control.TextAlignment = UITextAlignment.Center;
                    break;

                case "Justified":
                    UpdateTextOnControl();
                    break;

                default:
                    Control.TextAlignment = USER_LANGUAGE.Equals("1") ? UITextAlignment.Right : UITextAlignment.Left;
                    break;
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

        private void UpdateTextOnControl()
        {
            if (Control == null)
                return;

            //define paragraph-style
            var style = new NSMutableParagraphStyle()
            {
                Alignment = UITextAlignment.Justified,
                FirstLineHeadIndent = 0.001f,
            };

            //define attributes that use both paragraph-style, and font-style
            var uiAttr = new UIStringAttributes()
            {
                ParagraphStyle = style,
                BaselineOffset = 0,

                Font = Control.Font
            };

            //define frame to ensure justify alignment is applied
            Control.Frame = new RectangleF(0, 0, (float)Element.Width, (float)Element.Height);

            //set new text with ui-style-attributes to native control (UILabel)
            var stringToJustify = Control.Text ?? string.Empty;
            var attributedString = new Foundation.NSAttributedString(stringToJustify, uiAttr.Dictionary);
            Control.AttributedText = attributedString;
            Control.Lines = 0;
        }
    }
}