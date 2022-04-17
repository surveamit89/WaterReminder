using System;
using Xamarin.Essentials;

namespace WaterReminder.DataService
{
    public class AppDataKey
    {
        public static string IsProfileDataSaved = "is_profile_data_saved";
        public static string ProfileWakeUpTime = "profile_wakup";
        public static string ProfileBedTime = "profile_bedtime";
        public static string ProfileIntakeGoal = "profile_intake_goal";
        public static string ProfileWeight = "profile_weight";
        public static string ProfileGender = "profile_gender";
        public static string TipCounter = "tip_counter";
        public static string ProfileIntakeTaken = "profile_intake_taken";
        public static string IntakeCupSize = "intake_cup_size";
        public static string TodaysRecords = "todays_records";
        public static string ReminderSchedule = "reminder_schedule";
        public static string IsReminderScheduleSaved = "is_reminder_schedule_saved";
    }

    public class AppData
    {
        public static void SetIsProfileDataSaved(bool requestedValue)
        {
            Preferences.Set(AppDataKey.IsProfileDataSaved, requestedValue);
        }

        public static bool GetIsProfileDataSaved()
        {
            var response = Preferences.Get(AppDataKey.IsProfileDataSaved, false);
            return response;
        }

        public static void SetData(string requestedKey, string requestedValue)
        {
            Preferences.Set(requestedKey, requestedValue);
        }

        public static string GetData(string requestedKey)
        {
            var response = Preferences.Get(requestedKey, null);
            return response;
        }
        public static void SetIsReminderScheduleSaved(bool requestedValue)
        {
            Preferences.Set(AppDataKey.IsReminderScheduleSaved, requestedValue);
        }

        public static bool GetIsReminderScheduleSaved()
        {
            var response = Preferences.Get(AppDataKey.IsReminderScheduleSaved, false);
            return response;
        }
    }

}
