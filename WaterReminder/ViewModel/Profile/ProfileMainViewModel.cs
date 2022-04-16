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
            var selectedGoalIntake= AppData.GetData(AppDataKey.ProfileIntakeGoal);
            AppProfileData.SelectedIntakeGoal = string.IsNullOrEmpty(selectedGoalIntake) ? string.Format(_selectedValueFormat, Constant.IntakGoalRecommendation): string.Format(_selectedValueFormat, selectedGoalIntake);

            AppProfileData.SelectedGender = AppData.GetData(AppDataKey.ProfileGender);

            var selectedWeight = AppData.GetData(AppDataKey.ProfileWeight);
            AppProfileData.SelectedWeight = string.IsNullOrEmpty(selectedWeight) ? null: string.Format(Constant.WightFormat, selectedWeight);

            AppProfileData.SelectedWakeUpTime = AppData.GetData(AppDataKey.ProfileWakeUpTime);
            AppProfileData.SelectedBedTime = AppData.GetData(AppDataKey.ProfileBedTime);

        }
    }
}
