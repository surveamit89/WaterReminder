using System;
namespace WaterReminder.Model
{
    public class NotificationModel
    {
        public string Message { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public int NotificatioID { get; set; }
        public DateTime NotifyTime { get; set; }
    }
}
