using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using MvvmCross;
using MvvmCross.Commands;
using WaterReminder.DataService;
using WaterReminder.Model;
using WaterReminder.Utility;

namespace WaterReminder.ViewModel
{
    public class ReminderScheduleViewModel: BaseViewModel
    {
        private ICommand _closeCommand;
        private ICommand _selectScheduleCommand;
        private bool _isReminderOn { get; set; }

        public string PageTitle => "Reminder schedule";

        public bool IsReminderOn
        {
            get { return _isReminderOn; }
            set
            {
                _isReminderOn = value;
                RaisePropertyChanged(() => IsReminderOn);
                AppData.SetIsReminderScheduleSaved(IsReminderOn);
                CheckReminder();
            }
        }

        public ObservableCollection<ReminderScheduleModel> ReminderList { get; set; }

        public ReminderScheduleViewModel()
        {
            ReminderList = new ObservableCollection<ReminderScheduleModel> {
                new ReminderScheduleModel { DisplayMinutes = "1 min" ,SelectedMinutes=1, SelectScheduleCommand=SelectScheduleCommand },
                new ReminderScheduleModel { DisplayMinutes = "5 min" ,SelectedMinutes=5, SelectScheduleCommand=SelectScheduleCommand },
                new ReminderScheduleModel { DisplayMinutes = "10 min" ,SelectedMinutes=10, SelectScheduleCommand=SelectScheduleCommand },
                new ReminderScheduleModel { DisplayMinutes = "15 min" ,SelectedMinutes=15, SelectScheduleCommand=SelectScheduleCommand },
                new ReminderScheduleModel { DisplayMinutes = "20 min" ,SelectedMinutes=20, SelectScheduleCommand=SelectScheduleCommand },
                new ReminderScheduleModel { DisplayMinutes = "30 min" ,SelectedMinutes=30, SelectScheduleCommand=SelectScheduleCommand },
                new ReminderScheduleModel { DisplayMinutes = "60 min" ,SelectedMinutes=60, SelectScheduleCommand=SelectScheduleCommand }
            };
            IsReminderOn= AppData.GetIsReminderScheduleSaved();
        }

        private void CheckReminder()
        {
            if (IsReminderOn)
            {
                double reminderMinutes = Convert.ToDouble(AppData.GetData(AppDataKey.ReminderSchedule));
                if (reminderMinutes == 0)
                {
                    ReminderList[0].IsSelected = true;
                    ProcessSelectScheduleCommand(ReminderList[0]);
                }
                else
                {
                    var found = ReminderList.FirstOrDefault(a => a.SelectedMinutes == reminderMinutes);
                    if (found != null)
                    {
                        ProcessSelectScheduleCommand(found);
                    }
                }
            }
            else
            {
                AppData.SetData(AppDataKey.ReminderSchedule, null);
                Mvx.IoCProvider.Resolve<IStartService>().CancelBackgroundNotificationService(Constant.NotificationSericeID);
            }
        }

        public ICommand SelectScheduleCommand
        {
            get
            {
                return _selectScheduleCommand ?? (_selectScheduleCommand = new MvxCommand<ReminderScheduleModel>(ProcessSelectScheduleCommand));
            }
        }

        private void ProcessSelectScheduleCommand(ReminderScheduleModel selectedItem)
        {
            foreach (var item in ReminderList)
            {
                item.IsSelected = item.SelectedMinutes == selectedItem.SelectedMinutes ? true : false;
            }
            AppData.SetData(AppDataKey.ReminderSchedule, selectedItem.SelectedMinutes.ToString());
            Mvx.IoCProvider.Resolve<IStartService>().CancelBackgroundNotificationService(Constant.NotificationSericeID);
            Mvx.IoCProvider.Resolve<IStartService>().StartBackgroundNotificationService(Constant.NotificationSericeID, selectedItem.SelectedMinutes);
        }

        public ICommand CloseCommand
        {
            get
            {
                _closeCommand = _closeCommand ?? new MvxCommand(ProcessCloseCommand);
                return _closeCommand;
            }
        }

        private void ProcessCloseCommand()
        {
            NavigationService.Close(this);
        }
    }
}
