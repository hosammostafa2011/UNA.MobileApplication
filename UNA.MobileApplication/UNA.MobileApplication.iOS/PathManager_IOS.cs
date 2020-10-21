using System;
using System.IO;
using Xamarin.Forms;
using ObjCRuntime;
using System.Runtime.InteropServices;
using UIKit;
using System.Diagnostics;
using UNA.MobileApplication.iOS;
using UNA.MobileApplication.Interface;

[assembly: Dependency(typeof(PathManager_IOS))]

namespace UNA.MobileApplication.iOS
{
    public class PathManager_IOS : IPathManager
    {
        [DllImport(ObjCRuntime.Constants.ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
        internal extern static IntPtr IntPtr_objc_msgSend(IntPtr receiver, IntPtr selector, UISemanticContentAttribute arg1);

        public PathManager_IOS()
        {
        }

        public void SetLayoutRTL()
        {
            try
            {
                Selector selector = new Selector("setSemanticContentAttribute:");
                IntPtr_objc_msgSend(UIView.Appearance.Handle, selector.Handle, UISemanticContentAttribute.ForceRightToLeft);
            }
            catch (Exception s)
            {
                Debug.WriteLine("failed to set layout...." + s.Message.ToString());
            }
        }

        public void SetLayoutLTR()
        {
            try
            {
                Selector selector = new Selector("setSemanticContentAttribute:");
                IntPtr_objc_msgSend(UIView.Appearance.Handle, selector.Handle, UISemanticContentAttribute.ForceLeftToRight);
            }
            catch (Exception s)
            {
                Debug.WriteLine("failed to set layout...." + s.Message.ToString());
            }
        }
    }
}