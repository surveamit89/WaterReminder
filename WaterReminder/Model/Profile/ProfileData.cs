using System;
using System.Text;
using MvvmCross.ViewModels;
using WaterReminder.ViewModel;

namespace WaterReminder.Model.Profile
{
    public class ProfileData:MvxViewModel
    {
        private string _selectedUnit;
        public string SelectedUnit
        {
            get { return _selectedUnit; }
            set
            {
                _selectedUnit = value;
                RaisePropertyChanged(() => SelectedUnit);
            }
        }

        private string _selectedIntakeGoal;
        public string SelectedIntakeGoal
        {
            get { return _selectedIntakeGoal; }
            set
            {
                _selectedIntakeGoal = value;
                RaisePropertyChanged(() => SelectedIntakeGoal);
            }
        }

        private string _selectedGender;
        public string SelectedGender
        {
            get { return _selectedGender; }
            set
            {
                _selectedGender = value;
                RaisePropertyChanged(() => SelectedGender);
            }
        }

        //public string SelectedWeight { get; set; }
        private string _selectedWeight;
        public string SelectedWeight
        {
            get { return _selectedWeight; }
            set
            {
                _selectedWeight = value;
                RaisePropertyChanged(() => SelectedWeight);
            }
        }

        private string _selectedWakeUpTime;
        public string SelectedWakeUpTime
        {
            get { return _selectedWakeUpTime; }
            set
            {
                _selectedWakeUpTime = value;
                RaisePropertyChanged(() => SelectedWakeUpTime);
            }
        }

        private string _selectedBedTime;
        public string SelectedBedTime
        {
            get { return _selectedBedTime; }
            set
            {
                _selectedBedTime = value;
                RaisePropertyChanged(() => SelectedBedTime);
            }
        }
    }
}
