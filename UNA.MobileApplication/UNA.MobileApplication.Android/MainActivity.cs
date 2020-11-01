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
            /*Task.Run(() =>
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

            MessagingCenter.Subscribe<string, string>("MyApp", "Subscribe", async (sender, arg) =>
            {
                switch (arg.ToString())
                {
                    case "1":
                        CrossFirebasePushNotification.Current.Subscribe("1");
                        CrossFirebasePushNotification.Current.Unsubscribe(new string[] { "2", "3" });
                        break;

                    case "2":
                        CrossFirebasePushNotification.Current.Subscribe("2");
                        CrossFirebasePushNotification.Current.Unsubscribe(new string[] { "1", "3" });
                        break;

                    case "3":
                        CrossFirebasePushNotification.Current.Subscribe("3");
                        CrossFirebasePushNotification.Current.Unsubscribe(new string[] { "1", "2" });
                        break;

                    default:
                        CrossFirebasePushNotification.Current.Unsubscribe(new string[] { "1", "2", "3" });
                        break;
                }
            });
            */
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