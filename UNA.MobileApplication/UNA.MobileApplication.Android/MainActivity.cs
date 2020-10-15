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

namespace UNA.MobileApplication.Droid
{
    [Activity(Label = "UNA-OIC (يونا)", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
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

            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
                FirebasePushNotificationManager.DefaultNotificationChannelId = "FirebasePushNotificationChannel";

                FirebasePushNotificationManager.DefaultNotificationChannelName = "General";
            }

#if DEBUG
            FirebasePushNotificationManager.Initialize(this, true);
#else
                          FirebasePushNotificationManager.Initialize(this,false);
#endif

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