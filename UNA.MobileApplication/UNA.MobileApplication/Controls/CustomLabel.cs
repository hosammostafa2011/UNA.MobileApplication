using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace UNA.MobileApplication.Controls
{
    public class CustomLabel : Label
    {
        // make it bindable, shortened for simplicity here
        public double LineHeight { get; set; }
    }
}