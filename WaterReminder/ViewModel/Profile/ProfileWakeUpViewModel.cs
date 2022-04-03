using System;
using System.ComponentModel;
using System.Windows.Input;
using MvvmCross.Commands;
using WaterReminder.DataService;

namespace WaterReminder.ViewModel.Profile
{
    public class ProfileWakeUpViewModel : BaseViewModel
    {
        private DateTime _wakeUpTime;
        private ICommand _closeCommand;

        public ProfileWakeUpViewModel()
        {
            PropertyChanged += ProfileWakeUpViewModel_PropertyChanged;
        }

        private void ProfileWakeUpViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (WakeUpTime != DateTime.MinValue)
            {
                AppData.SetProfileData(AppDataKey.ProfileWakeUpTime, WakeUpTime.ToString("hh:mm tt"));
            }
        }

        public DateTime WakeUpTime
        {
            get { return _wakeUpTime; }
            set
            {
                _wakeUpTime = value;
                RaisePropertyChanged(() => WakeUpTime);
            }
        }

        //command
        public ICommand CloseCommand
        {
            get
            {
                _closeCommand = _closeCommand ?? new MvxCommand<bool>(ClosePage);
                return _closeCommand;
            }
        }

        private void ClosePage(bool isDataUpdated)
        {
            NavigationService.Close(this, isDataUpdated);
        }
    }
}
