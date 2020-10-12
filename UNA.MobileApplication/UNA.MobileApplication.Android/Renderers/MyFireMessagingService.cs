using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Messaging;

namespace UNA.MobileApplication.Droid.Renderers
{
    [Service]
    [IntentFilter(new[] {
        "com.google.firebase.MESSAGING_EVENT"
    })]
    internal class MyFireMessagingService : FirebaseMessagingService
    {
        public override void OnMessageReceived(RemoteMessage message)
        {
            base.OnMessageReceived(message);
            ICollection<string> lstKeys = message.Data.Keys;
            ICollection<string> lstValues = message.Data.Values;

            string body = message.Data["body"];
            string title = message.Data["title"];

            new NotificationHelper().CreateNotification(title, body);

            // Send Acknowldge to server to check alive

            //SendNotificatios(body, title);
            //SendNotificatios(message.GetNotification().Body, message.GetNotification().Title);
        }

        public void SendNotificatios(string body, string Header)
        {
            Notification.Builder builder = new Notification.Builder(this);
            builder.SetSmallIcon(Resource.Drawable.icon);
            var intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            PendingIntent pendingIntent = PendingIntent.GetActivity(this, 0, intent, 0);
            builder.SetContentIntent(pendingIntent);
            builder.SetLargeIcon(BitmapFactory.DecodeResource(Resources, Resource.Drawable.icon));
            builder.SetContentTitle(Header);
            builder.SetContentText(body);
            builder.SetDefaults(NotificationDefaults.Sound);
            builder.SetAutoCancel(true);
            NotificationManager notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.Notify(1, builder.Build());
        }
    }
}