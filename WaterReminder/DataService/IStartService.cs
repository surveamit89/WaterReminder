using System;
namespace WaterReminder.DataService
{
    public interface IStartService
    {
        void StartBackgroundNotificationService(int id);
        void CancelBackgroundNotificationService(int id);
    }
}
