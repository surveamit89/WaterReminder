using System;
namespace WaterReminder.DataService
{
    public interface IStartService
    {
        void StartBackgroundNotificationService(int id,int min);
        void CancelBackgroundNotificationService(int id);
    }
}
