using System;
using System.ComponentModel;
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

        public ProfileViewModel()
        {
            AppProfileData = new ProfileData();
            AppProfileData.SelectedUnit = Constant.SelectedUnit;
            VerifyProfileData();
            LoadProfileData();
        }

        private void VerifyProfileData()
        {
            if (AppData.GetIsProfileDataSaved())
            {
                CloseVisibility = AppData.GetIsProfileDataSaved();
            }
            ButtonEnabled = true;
        }

        //properties

        public bool ButtonEnabled
        {
            get => _buttonEnabled;
            set => _buttonEnabled = value;
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
        }
    }
}
