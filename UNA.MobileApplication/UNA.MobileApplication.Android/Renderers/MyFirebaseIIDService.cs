using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Firebase.Iid;
using Firebase.Messaging;
using Plugin.SecureStorage;
using Xamarin.Forms;

namespace UNA.MobileApplication.Droid.Renderers
{
    [Service]
    [IntentFilter(new[] {
        "com.google.firebase.INSTANCE_ID_EVENT"
    })]

    internal class MyFirebaseIIDService : FirebaseMessagingService
    {
        private const string TAG = "MyFirebaseIIDService";
        public override void OnNewToken(string p0)
        {
            base.OnNewToken(p0);
            SendRegistrationToServer(p0);
            MessagingCenter.Send<string, string>("MyApp", "TokenChanges", p0);

        }
        private void SendRegistrationToServer(string token)
        {
            if (!string.IsNullOrEmpty(token))
                CrossSecureStorage.Current.SetValue("FCMToken", token);
        }
    }
}