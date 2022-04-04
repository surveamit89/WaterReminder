using System;
using System.Linq;
using WaterReminder.DataService;
using WaterReminder.Model.Profile;
using WaterReminder.Utility;

namespace WaterReminder.ViewModel.Profile
{
    public class ProfileMainViewModel : BaseViewModel
    {
        private readonly string _selectedValueFormat = Constant.IntakGoalFormat;

        private bool _isAllDataSet;

        public bool IsAllDataSet
        {
            get { return _isAllDataSet; }
            set
            {
                _isAllDataSet = value;
                RaisePropertyChanged(() => IsAllDataSet);
            }
        }

        private ProfileData _appProfileData;

        public ProfileData AppProfileData
        {
            get { return _appProfileData; }
            set
            {
                _appProfileData = value;
                RaisePropertyChanged(() => AppProfileData);
            }
        }

        public void LoadProfileData()
        {
            AppProfileData.SelectedUnit = Constant.SelectedUnit;
            var selectedGoalIntake= AppData.GetProfileData(AppDataKey.ProfileIntakeGoal);
            AppProfileData.SelectedIntakeGoal = string.IsNullOrEmpty(selectedGoalIntake) ? string.Format(_selectedValueFormat, Constant.IntakGoalRecommendation): string.Format(_selectedValueFormat, selectedGoalIntake);

            AppProfileData.SelectedGender = AppData.GetProfileData(AppDataKey.ProfileGender);

            var selectedWeight = AppData.GetProfileData(AppDataKey.ProfileWeight);
            AppProfileData.SelectedWeight = string.IsNullOrEmpty(selectedWeight) ? null: string.Format(Constant.WightFormat, selectedWeight);

            AppProfileData.SelectedWakeUpTime = AppData.GetProfileData(AppDataKey.ProfileWakeUpTime);
            AppProfileData.SelectedBedTime = AppData.GetProfileData(AppDataKey.ProfileBedTime);

        }
    }
}
