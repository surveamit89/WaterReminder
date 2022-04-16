using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.Text;
using Java.Lang;
using Newtonsoft.Json;
using WaterReminder.DataService;
using WaterReminder.Model;

namespace WaterReminder.Android
{
    public class StartServiceAndroid : IStartService
    {
        internal string _randomNumber;
        readonly DateTime _jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public void StartBackgroundNotificationService(int id)
        {
            Random generator = new Random();
            _randomNumber = generator.Next(100000, 999999).ToString("D6");

            long repeateForMinute = 10000; // In milliseconds   
            long totalMilliSeconds = (long)(DateTime.Now.ToUniversalTime() - _jan1st1970).TotalMilliseconds;
            if (totalMilliSeconds < JavaSystem.CurrentTimeMillis())
            {
                totalMilliSeconds = totalMilliSeconds + repeateForMinute;
            }

            var intent = CreateIntent(id);

            var pendingIntent = PendingIntent.GetBroadcast(Application.Context, Convert.ToInt32(_randomNumber), intent, PendingIntentFlags.Immutable);
            var alarmManager = GetAlarmManager();
            alarmManager.SetRepeating(AlarmType.RtcWakeup, totalMilliSeconds, repeateForMinute, pendingIntent);

        }

        public void CancelBackgroundNotificationService(int id)
        {
            var intent = CreateIntent(id);
            var pendingIntent = PendingIntent.GetBroadcast(Application.Context, Convert.ToInt32(_randomNumber), intent, PendingIntentFlags.Immutable);
            var alarmManager = GetAlarmManager();
            alarmManager.Cancel(pendingIntent);
            var notificationManager = NotificationManagerCompat.From(Application.Context);
            notificationManager.CancelAll();
            notificationManager.Cancel(id);
        }

        private Intent CreateIntent(int id)
        {

            return new Intent(Application.Context, typeof(ScheduledAlarmHandler))
                .SetAction("LocalNotifierIntent" + id);
        }

        private AlarmManager GetAlarmManager()
        {

            var alarmManager = Application.Context.GetSystemService(Context.AlarmService) as AlarmManager;
            return alarmManager;
        }
    }

    public static class GenerateNotification
    {
        public static void SendNotification()
        {
            var notification = new NotificationModel();
            notification.Title = "Water Reminder";
            notification.Message = DateTime.Now.ToString("hh:mm tt");


            Intent intent = new Intent(Application.Context, typeof(SplashActivity));
            intent.PutExtra("is_from_notification", true);
            intent.PutExtra("notification_json_string", JsonConvert.SerializeObject(notification));
            intent.AddFlags(ActivityFlags.ClearTop);
            var pendingIntent = PendingIntent.GetActivity(Application.Context, Convert.ToInt32(100), intent, PendingIntentFlags.OneShot);

            ProcessNotificationOnDevice(pendingIntent, notification);
        }

        static void ProcessNotificationOnDevice(PendingIntent pendingIntent, NotificationModel not)
        {
            NotificationCompat.Builder notificationBuilder;
            string channelId = "reminder_notification_channel";
            string channelName = "Reminder_Notification_Channel";
            var notificationManager = NotificationManager.FromContext(Application.Context);
            notificationManager.CancelAll();
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
            try
            {
                notificationBuilder.SetContentText(not.Message);
                string htmlText = "<b>" + not.Title + "</b>";
                notificationBuilder.SetContentTitle(HtmlCompat.FromHtml(htmlText, 0));
            }
            catch (System.Exception)
            {
                notificationBuilder.SetContentText(not.Message);
                string htmlText = "<b>" + not.Title + "</b>";
                notificationBuilder.SetContentTitle(HtmlCompat.FromHtml(htmlText, 0));
            }

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

            notificationBuilder.SetStyle(new NotificationCompat.BigTextStyle());
            notificationBuilder.SetAutoCancel(true);
            notificationBuilder.SetContentIntent(pendingIntent);
            notificationBuilder.SetVisibility((int)NotificationVisibility.Public);
            notificationBuilder.SetPriority((int)NotificationCompat.PriorityHigh);

            //Android.Net.Uri soundUri = Android.Net.Uri.Parse(ContentResolver.SchemeAndroidResource + "://" + Application.PackageName + "/" + Resource.Raw.notification);

            //notificationBuilder.SetSound(soundUri);

            var notification = notificationBuilder.Build();

            notificationManager.Notify(Convert.ToInt32(not.NotificatioID), notification);
        }
    }
}
