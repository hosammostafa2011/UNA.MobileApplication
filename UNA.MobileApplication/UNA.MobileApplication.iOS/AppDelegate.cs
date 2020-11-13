using System;
using System.Collections.Generic;
using System.Linq;
using Firebase.CloudMessaging;
using FirebaseAdmin.Messaging;
using Foundation;
using LabelHtml.Forms.Plugin.iOS;
using Plugin.FirebasePushNotification;
using Plugin.SecureStorage;
using UIKit;
using UserNotifications;
using Xamarin.Forms;

namespace UNA.MobileApplication.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Xamarin.Forms.Forms.SetFlags(new string[] { "CarouselView_Experimental", "IndicatorView_Experimental" });
            HtmlLabelRenderer.Initialize();

            global::Xamarin.Forms.Forms.Init();
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();
            PathManager_IOS pathManager = new PathManager_IOS();

            LoadApplication(new App());
            

            //FirebasePushNotificationManager.Initialize(options, true);
            //CrossFirebasePushNotification.Current.RegisterForPushNotifications();

            if (true)
            {
                FirebasePushNotificationManager.Initialize(options, new NotificationUserCategory[]
                            {
                new NotificationUserCategory("message",new List<NotificationUserAction> {
                    new NotificationUserAction("Reply","Reply",NotificationActionType.Foreground)
                }),
                new NotificationUserCategory("request",new List<NotificationUserAction> {
                    new NotificationUserAction("Accept","Accept"),
                    new NotificationUserAction("Reject","Reject",NotificationActionType.Destructive)
                })
                            });

                var notificationSettings = UIUserNotificationSettings.GetSettingsForTypes(
                    UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound, null
                );
                app.RegisterUserNotificationSettings(notificationSettings);

                if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
                {
                    UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Alert | UNAuthorizationOptions.Sound | UNAuthorizationOptions.Sound,
                                                                            (granted, error) =>
                                                                            {
                                                                                if (granted)
                                                                                    InvokeOnMainThread(UIApplication.SharedApplication.RegisterForRemoteNotifications);
                                                                            });
                }
                else if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
                {
                    var pushSettings = UIUserNotificationSettings.GetSettingsForTypes(
                            UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound,
                            new NSSet());

                    UIApplication.SharedApplication.RegisterUserNotificationSettings(pushSettings);
                    UIApplication.SharedApplication.RegisterForRemoteNotifications();
                }
                else
                {
                    UIRemoteNotificationType notificationTypes = UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge | UIRemoteNotificationType.Sound;
                    UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(notificationTypes);
                }
            }
            CrossFirebasePushNotification.Current.RegisterForPushNotifications();

            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                CrossSecureStorage.Current.SetValue("FCMToken", p.Token);
                MessagingCenter.Send<string, string>("MyApp", "TokenChanges", p.Token);

            };
            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {
                Dictionary<string, object> dic = p.Data as Dictionary<string, object>;
                System.Diagnostics.Debug.WriteLine("Received");
                FirebasePushNotificationManager.CurrentNotificationPresentationOption = UNNotificationPresentationOptions.Alert;
                
            };

            CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
            {
                
            };

            return base.FinishedLaunching(app, options);
        }


        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            FirebasePushNotificationManager.DidRegisterRemoteNotifications(deviceToken);
        }

        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        {
            FirebasePushNotificationManager.RemoteNotificationRegistrationFailed(error);

        }
        // To receive notifications in foregroung on iOS 9 and below.
        // To receive notifications in background in any iOS version
        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
            // If you are receiving a notification message while your app is in the background,
            // this callback will not be fired 'till the user taps on the notification launching the application.

            // If you disable method swizzling, you'll need to call this method. 
            // This lets FCM track message delivery and analytics, which is performed
            // automatically with method swizzling enabled.
            FirebasePushNotificationManager.DidReceiveMessage(userInfo);
            // Do your magic to handle the notification data
            System.Console.WriteLine(userInfo);

            completionHandler(UIBackgroundFetchResult.NewData);
        }



    }
}