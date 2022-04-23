using System;
using System.IO;
using System.Xml.Serialization;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.Text;
using WaterReminder.Model;

namespace WaterReminder.Android
{
    [BroadcastReceiver(Enabled = true, Label = "Local Notifications Broadcast Receiver")]
    public class ScheduledAlarmHandler : BroadcastReceiver
    {
        //public override void OnReceive(Context context, Intent intent)
        //{
        //    PowerManager pm = (PowerManager)context.GetSystemService(Context.PowerService);
        //    PowerManager.WakeLock wakeLock = pm.NewWakeLock(WakeLockFlags.Partial, "ScheduledAlarmHandler");
        //    wakeLock.Acquire();

        //    // Run your code here
        //    GenerateNotification.SendNotification(context, intent);
        //    wakeLock.Release();

        //}
        public const string LocalNotificationKey = "LocalNotification";

        public override void OnReceive(Context context, Intent intent)
        {
            var extra = intent.GetStringExtra(LocalNotificationKey);
            var notification = DeserializeNotification(extra);
            NotificationCompat.Builder notificationBuilder;
            string channelId = "reminder_notification_channel";
            string channelName = "Reminder_Notification_Channel";
            var notificationManager = NotificationManager.FromContext(Application.Context);
            //Generating notification

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {

                var notificationChannel = new NotificationChannel(channelId,
                                                                  CharSequence.ArrayFromStringArray(new string[] { channelName })[0],
                                                                  NotificationImportance.High);
                notificationChannel.EnableLights(true);
                notificationChannel.EnableVibration(true);
                notificationManager.CreateNotificationChannel(notificationChannel);
                notificationBuilder = new NotificationCompat.Builder(Application.Context, channelId);
            }
            else
            {
                notificationBuilder = new NotificationCompat.Builder(Application.Context, channelId);
            }
            //var builder = new NotificationCompat.Builder(Application.Context, channelId)
            //    .SetContentTitle(notification.Title)
            //    .SetContentText(notification.Message)
            //    .SetSmallIcon(Resource.Mipmap.ic_launcher)
            //    .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Ringtone))
            //    .SetAutoCancel(true);

            try
            {
                notificationBuilder.SetContentText(notification.Message+" "+DateTime.Now.ToString("hh:mm:ss tt"));
                string htmlText = "<b>" + notification.Title + "</b>";
                notificationBuilder.SetContentTitle(HtmlCompat.FromHtml(htmlText, 0));
            }
            catch (System.Exception)
            {
                notificationBuilder.SetContentText(notification.Message+" " + DateTime.Now.ToString("hh:mm:ss tt"));
                string htmlText = "<b>" + notification.Title + "</b>";
                notificationBuilder.SetContentTitle(HtmlCompat.FromHtml(htmlText, 0));
            }

            try
            {
                if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                {
                    notificationBuilder.SetSmallIcon(Resource.Mipmap.ic_launcher);
                    notificationBuilder.SetColor(Resource.Color.colorAccent);
                }
                else
                {
                    notificationBuilder.SetSmallIcon(Resource.Mipmap.ic_launcher);
                    notificationBuilder.SetLargeIcon(BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Mipmap.ic_launcher));
                }
            }
            catch (Exception ex)
            {

            }

            notificationBuilder.SetStyle(new NotificationCompat.BigTextStyle());
            notificationBuilder.SetAutoCancel(true);
            //notificationBuilder.SetContentIntent(pendingIntent);
            notificationBuilder.SetVisibility((int)NotificationVisibility.Public);
            notificationBuilder.SetPriority((int)NotificationCompat.PriorityHigh);

            var resultIntent = StartServiceAndroid.GetLauncherActivity();
            resultIntent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
            var stackBuilder = AndroidX.Core.App.TaskStackBuilder.Create(Application.Context);
            stackBuilder.AddNextIntent(resultIntent);

            Random random = new Random();
            int randomNumber = random.Next(9999 - 1000) + 1000;

            var resultPendingIntent =
                stackBuilder.GetPendingIntent(randomNumber, (int)PendingIntentFlags.Immutable);
            notificationBuilder.SetContentIntent(resultPendingIntent);
            // Sending notification    
            //var notificationManager = NotificationManagerCompat.From(Application.Context);
            notificationManager.Notify(randomNumber, notificationBuilder.Build());
        }

        private NotificationModel DeserializeNotification(string notificationString)
        {

            var xmlSerializer = new XmlSerializer(typeof(NotificationModel));
            using (var stringReader = new StringReader(notificationString))
            {
                var notification = (NotificationModel)xmlSerializer.Deserialize(stringReader);
                return notification;
            }
        }
    }
}
