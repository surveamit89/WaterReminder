using System;
using Android.App;
using Android.OS;
using Android.Text.Format;
using Android.Widget;
using MvvmCross.Platforms.Android.Views.Fragments;
using WaterReminder.DataService;
using WaterReminder.ViewModel.Profile;

namespace WaterReminder.Android.Activity.Profile
{
    public class ProfileBedTimeDialogueActivity : MvxDialogFragment<ProfileBedTimeViewModel>, TimePickerDialog.IOnTimeSetListener
    {
        public static readonly string TAG = "MyTimePickerFragment";
        Action<DateTime> timeSelectedHandler = delegate { };

        public static ProfileBedTimeDialogueActivity NewInstance(Action<DateTime> onTimeSelected)
        {
            ProfileBedTimeDialogueActivity frag = new ProfileBedTimeDialogueActivity();
            frag.timeSelectedHandler = onTimeSelected;
            return frag;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            DateTime currentTime =string.IsNullOrEmpty(AppData.GetProfileData(AppDataKey.ProfileBedTime)) ? DateTime.Now:DateTime.Parse(AppData.GetProfileData(AppDataKey.ProfileBedTime));
            bool is24HourFormat = DateFormat.Is24HourFormat(Activity);
            TimePickerDialog dialog = new TimePickerDialog
                (Activity, this, currentTime.Hour, currentTime.Minute, is24HourFormat);
            return dialog;
        }

        public void OnTimeSet(TimePicker view, int hourOfDay, int minute)
        {
            DateTime currentTime = DateTime.Now;
            DateTime selectedTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, hourOfDay, minute, 0);
            ViewModel.BedTime = selectedTime;
            timeSelectedHandler(selectedTime);
            ViewModel.CloseCommand.Execute(true);
        }
    }
}
