using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using MvvmCross.Commands;
using WaterReminder.DataService;
using WaterReminder.Model.Profile;
using WaterReminder.Utility;

namespace WaterReminder.ViewModel.Profile
{
    public class ProfileIntakeGoalViewModel : BaseViewModel
    {
        public Action<int> RefreshSelectedValue;
        private int _selectedValue;
        private string _displaySelectedValue;
        private ICommand _closeCommand;
        private ICommand _valueSelectedCommand;
        private ICommand _refreshCommand;
        private ICommand _showRecommendedCommand;

        public ProfileIntakeGoalViewModel()
        {
            RefreshValue();
        }
        
        public int SelectedValue
        {
            get { return _selectedValue; }
            set
            {
                _selectedValue = value;
                if (value>0)
                {
                    DisplaySelectedValue =string.Format(Constant.IntakGoalFormat, SelectedValue);
                }
                RaisePropertyChanged(() => SelectedValue);
            }
        }

        public string DisplaySelectedValue
        {
            get { return _displaySelectedValue; }
            set
            {
                _displaySelectedValue = value;
                RaisePropertyChanged(() => DisplaySelectedValue);
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

        public ICommand RefreshCommand
        {
            get
            {
                _refreshCommand = _refreshCommand ?? new MvxCommand(RefreshValue);
                return _refreshCommand;
            }
        }

        public ICommand ShowRecommendedCommand
        {
            get
            {
                _showRecommendedCommand = _showRecommendedCommand ?? new MvxCommand(ShowRecommended);
                return _showRecommendedCommand;
            }
        }

        private void ShowRecommended()
        {
            SelectedValue = Constant.IntakGoalRecommendation;
            RefreshSelectedValue?.Invoke(SelectedValue);
        }

        private void RefreshValue()
        {
            if (string.IsNullOrEmpty(AppData.GetData(AppDataKey.ProfileIntakeGoal)))
            {
                SelectedValue = Constant.IntakGoalRecommendation;
            }
            else
            {
                SelectedValue = Convert.ToInt32(AppData.GetData(AppDataKey.ProfileIntakeGoal));
            }
            RefreshSelectedValue?.Invoke(SelectedValue);
        }

        private void ClosePage(bool isDataUpdated)
        {
            if (isDataUpdated)
            {
                AppData.SetData(AppDataKey.ProfileIntakeGoal, SelectedValue.ToString());
            }
            NavigationService.Close(this, isDataUpdated);
        }
    }
}
