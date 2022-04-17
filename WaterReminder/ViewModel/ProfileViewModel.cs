using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Commands;
using WaterReminder.DataService;
using WaterReminder.Model.Profile;
using WaterReminder.Utility;
using WaterReminder.ViewModel.Profile;

namespace WaterReminder.ViewModel
{
    public class ProfileViewModel: ProfileMainViewModel
    {
        public Action<ProfileDialogueEnum> OpenDialogueAction;
        private bool _buttonEnabled;
        private bool _closeVisibility;
        private ICommand _saveContinueCommand;
        private ICommand _closeCommand;
        private ICommand _openDialogueCommand;
        private ICommand _scheduleReminderCommand;


        public ProfileViewModel()
        {
            AppProfileData = new ProfileData();
            AppProfileData.SelectedUnit = Constant.SelectedUnit;
            LoadProfileData();
            if (AppData.GetIsProfileDataSaved())
            {
                CloseVisibility = AppData.GetIsProfileDataSaved();
            }
            VerifyProfileData();
        }
        //properties

        public bool ButtonEnabled
        {
            get
            {
                return _buttonEnabled;
            }
            set
            {
                _buttonEnabled = value;
                RaisePropertyChanged(() => ButtonEnabled);
            }
        }
        public bool CloseVisibility
        {
            get => _closeVisibility;
            set => _closeVisibility = value;
        }

        //command
        public ICommand CloseCommand
        {
            get
            {
                _closeCommand = _closeCommand ?? new MvxCommand(ClosePage);
                return _closeCommand;
            }
        }
        public ICommand SaveContinueCommand
        {
            get
            {
                _saveContinueCommand = _saveContinueCommand ?? new MvxAsyncCommand(SaveContinue);
                return _saveContinueCommand;
            }
        }

        public ICommand OpenDialogueCommand
        {
            get
            {
                _openDialogueCommand = _openDialogueCommand ?? new MvxAsyncCommand<ProfileDialogueEnum>(OpenDialogue);
                return _openDialogueCommand;
            }
        }

        public ICommand ScheduleReminderCommand
        {
            get
            {
                _scheduleReminderCommand = _scheduleReminderCommand ?? new MvxAsyncCommand(ScheduleReminder);
                return _scheduleReminderCommand;
            }
        }

        private async Task ScheduleReminder()
        {
            await NavigationService.Navigate<ReminderScheduleViewModel>();
        }

        private async Task SaveContinue()
        {
            AppData.SetIsProfileDataSaved(true);
            await NavigationService.Navigate<MainViewModel>();
        }

        private void ClosePage()
        {
            NavigationService.Close(this);
        }

        private async Task OpenDialogue(ProfileDialogueEnum seklectedValue)
        {
            bool isDataUpdated = false;
            switch (seklectedValue)
            {
                case ProfileDialogueEnum.Gender:
                    isDataUpdated = await NavigationService.Navigate<ProfileGenderViewModel,bool>();
                    break;
                case ProfileDialogueEnum.Weight:
                    isDataUpdated = await NavigationService.Navigate<ProfileWeightViewModel,bool>();
                    break;
                case ProfileDialogueEnum.WakeUpTime:
                    isDataUpdated= await NavigationService.Navigate<ProfileWakeUpViewModel,bool>();
                    break;
                case ProfileDialogueEnum.BedTime:
                    isDataUpdated = await NavigationService.Navigate<ProfileBedTimeViewModel,bool>();
                    break;
                case ProfileDialogueEnum.IntakeGoal:
                    isDataUpdated = await NavigationService.Navigate<ProfileIntakeGoalViewModel, bool>();
                    break;
                //case ProfileDialogueEnum.Unit:
                //    isDataUpdated = await NavigationService.Navigate<ProfileUnitViewModel>();
                //    break;
            }

            if (isDataUpdated)
            {
                LoadProfileData();
            }
            VerifyProfileData();
        }

        private void VerifyProfileData()
        {
            bool isNullValue = AppProfileData.GetType().GetProperties()
                .Where(pi => pi.PropertyType == typeof(string))
                .Select(pi => (string)pi.GetValue(AppProfileData))
                .Any(value => string.IsNullOrEmpty(value));
            ButtonEnabled = !isNullValue;
        }
    }
}
