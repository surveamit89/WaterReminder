using System;
using System.IO;
using System.Xml.Serialization;
using Android.App;
using Android.Content;
using Android.Media;
using Android.Support.V4.App;
using WaterReminder.Model;
using WaterReminder.Utility;

namespace WaterReminder.Android
{
    [BroadcastReceiver(Enabled = true, Label = "Local Notifications Broadcast Receiver")]
    public class ScheduledAlarmHandler : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            GenerateNotification.SendNotification(context,intent);
        }
    }
}
