using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Acr.UserDialogs;
using LabelHtml.Forms.Plugin.Droid;
using System.Threading.Tasks;
using Firebase.Iid;
using Plugin.FirebasePushNotification;
using Firebase.Messaging;
using Android.Content;
using Xamarin.Forms;
using Firebase;
using System.Collections.Generic;
using Plugin.SecureStorage;

namespace UNA.MobileApplication.Droid
{
    [Activity(Label = "UNA-OIC (يونا)",
        Icon = "@drawable/icon",
        Theme = "@style/MainTheme",
        MainLauncher = true,
         NoHistory = true
        //ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize
        )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            Window.DecorView.LayoutDirection = LayoutDirection.Rtl;
            base.OnCreate(savedInstanceState);
            Task.Run(() =>
            {
                var instanceid = FirebaseInstanceId.Instance;
                instanceid.DeleteInstanceId();
            });

            //FirebaseApp.InitializeApp(this);
            //Set the default notification channel for your app when running Android Oreo
            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
                //Change for your default notification channel id here
                FirebasePushNotificationManager.DefaultNotificationChannelId = "FirebasePushNotificationChannel";

                //Change for your default notification channel name here
                FirebasePushNotificationManager.DefaultNotificationChannelName = "General";
            }

            //FirebasePushNotificationManager.Initialize(this, false);

            //If debug you should reset the token each time.
#if DEBUG
            FirebasePushNotificationManager.Initialize(this, true);
#else
                          FirebasePushNotificationManager.Initialize(this,false);
#endif
            //Handle notification when app is closed here
            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {
            };
            CrossFirebasePushNotification.Current.RegisterForPushNotifications();

            //CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            //{
            //    CrossSecureStorage.Current.SetValue("FCMToken", p.Token);
            //    MessagingCenter.Send<string, string>("MyApp", "TokenChanges", p.Token);
            //};
            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {
                Dictionary<string, object> dic = p.Data as Dictionary<string, object>;
                System.Diagnostics.Debug.WriteLine("Received");
                //FirebasePushNotificationManager.CurrentNotificationPresentationOption = UNNotificationPresentationOptions.Alert;
            };

            CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
            {
            };

            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            HtmlLabelRenderer.Initialize();
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            UserDialogs.Init(this);
            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}