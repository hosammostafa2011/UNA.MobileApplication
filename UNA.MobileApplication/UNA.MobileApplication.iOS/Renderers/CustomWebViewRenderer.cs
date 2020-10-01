using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using UNA.MobileApplication.Controls;
using UNA.MobileApplication.iOS.Renderers;
using WebKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomWebView), typeof(CustomWebViewRenderer))]

namespace UNA.MobileApplication.iOS.Renderers
{
    public class CustomWebViewRenderer : ViewRenderer<CustomWebView, WKWebView>
    {
        private WKWebView _wkWebView;

        protected override void OnElementChanged(ElementChangedEventArgs<CustomWebView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                var config = new WKWebViewConfiguration();
                _wkWebView = new WKWebView(Frame, config);
                SetNativeControl(_wkWebView);
            }
            if (e.NewElement != null)
            {
                if (!string.IsNullOrEmpty(Element.Url))
                    Control.LoadRequest(new NSUrlRequest(new NSUrl(Element.Url)));
                else
                    if (!string.IsNullOrEmpty(Element.Html))
                {
                    NSUrl baseurl = new NSUrl(NSBundle.MainBundle.BundlePath, true);
                    Control.LoadHtmlString(Element.Html, baseurl);
                }
            }
        }
    }
}