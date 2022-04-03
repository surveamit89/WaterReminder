using System;
using System.ComponentModel;
using System.Windows.Input;
using MvvmCross.Commands;
using WaterReminder.DataService;

namespace WaterReminder.ViewModel.Profile
{
    public class ProfileBedTimeViewModel : BaseViewModel
    {
        private DateTime _bedTime;
        private ICommand _closeCommand;

        public ProfileBedTimeViewModel()
        {
            PropertyChanged += ProfileWakeUpViewModel_PropertyChanged;
        }

        private void ProfileWakeUpViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (BedTime != DateTime.MinValue)
            {
                AppData.SetProfileData(AppDataKey.ProfileBedTime, BedTime.ToString("hh:mm tt"));
            }
        }

        public DateTime BedTime
        {
            get { return _bedTime; }
            set
            {
                _bedTime = value;
                RaisePropertyChanged(() => BedTime);
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
