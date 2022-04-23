using System;
using System.IO;
using System.Xml.Serialization;
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
        //internal string _randomNumber;
        //readonly DateTime _jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        //public void StartBackgroundNotificationService(int id,int min)
        //{
        //    Random generator = new Random();
        //    _randomNumber = generator.Next(100000, 999999).ToString("D6");

        //    long repeateForMinute =min * 60000; // In milliseconds   
        //    long totalMilliSeconds = (long)(DateTime.Now.ToUniversalTime() - _jan1st1970).TotalMilliseconds;
        //    if (totalMilliSeconds < JavaSystem.CurrentTimeMillis())
        //    {
        //        totalMilliSeconds = totalMilliSeconds + repeateForMinute;
        //    }

        //    var intent = CreateIntent(id);

        //    var pendingIntent = PendingIntent.GetBroadcast(Application.Context, Convert.ToInt32(_randomNumber), intent, PendingIntentFlags.Immutable);
        //    var alarmManager = GetAlarmManager();
        //    alarmManager.SetRepeating(AlarmType.RtcWakeup, totalMilliSeconds, repeateForMinute, pendingIntent);

        //}
        int _notificationIconId { get; set; }
        readonly DateTime _jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        internal string _randomNumber;
        DateTime notifyTime = DateTime.Now;

        public void StartBackgroundNotificationService(int id, int min)
        {
            //long repeateDay = 1000 * 60 * 60 * 24;    
            long repeateForMinute = min* 60000; // In milliseconds   
            long totalMilliSeconds = (long)(notifyTime.ToUniversalTime() - _jan1st1970).TotalMilliseconds;
            if (totalMilliSeconds < JavaSystem.CurrentTimeMillis())
            {
                totalMilliSeconds = totalMilliSeconds + repeateForMinute;
            }

            var intent = CreateIntent(id);
            var localNotification = new NotificationModel();
            localNotification.Title = "Water Reminder";
            localNotification.Message = "It's time to drink water";
            localNotification.NotifyTime = notifyTime;

            //if (_notificationIconId != 0)
            //{
            //    localNotification.IconId = _notificationIconId;
            //}
            //else
            //{
            //    localNotification.IconId = Resource.Drawable.notificationgrey;
            //}

            var serializedNotification = SerializeNotification(localNotification);
            intent.PutExtra(ScheduledAlarmHandler.LocalNotificationKey, serializedNotification);

            Random generator = new Random();
            _randomNumber = generator.Next(100000, 999999).ToString("D6");

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
        }

        public static Intent GetLauncherActivity()
        {
            var packageName = Application.Context.PackageName;
            return Application.Context.PackageManager.GetLaunchIntentForPackage(packageName);
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

        private string SerializeNotification(NotificationModel notification)
        {

            var xmlSerializer = new XmlSerializer(notification.GetType());

            using (var stringWriter = new StringWriter())
            {
                xmlSerializer.Serialize(stringWriter, notification);
                return stringWriter.ToString();
            }
        }
    }

    public static class GenerateNotification
    {
        public static void SendNotification(Context context, Intent intent)
        {
            if (AppData.GetIsReminderScheduleSaved())
            {
                var notification = new NotificationModel();
                notification.Title = "Water Reminder";
                //notification.Message = DateTime.Now.ToString("hh:mm tt");
                notification.Message = "It's time to drink water";


                //Intent intent = new Intent(Application.Context, typeof(SplashActivity));
                intent.PutExtra("is_from_notification", true);
                intent.PutExtra("notification_json_string", JsonConvert.SerializeObject(notification));
                intent.AddFlags(ActivityFlags.ClearTop);
                var pendingIntent = PendingIntent.GetActivity(context, Convert.ToInt32(100), intent, PendingIntentFlags.OneShot);

                ProcessNotificationOnDevice(pendingIntent, notification);
            }
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
            Random random = new Random();
            //int randomNumber = random.Next(9999 - 1000) + 1000;
            notificationManager.Notify(Convert.ToInt32(not.NotificatioID), notification);
            //notificationManager.Notify(Convert.ToInt32(not.NotificatioID), notification);
        }
    }
}
