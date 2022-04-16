using System;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Commands;
using WaterReminder.DataService;
using Xamarin.Essentials;

namespace WaterReminder.ViewModel
{
    public class HomeViewModel : BaseViewModel
    {

        //Property
        private double _intakeWater;
        private double _totalIntakeWater;
        private double _selectedQuantityWater;
        private string _homeTip;
        private ICommand _drunkWaterCommand;
        private ICommand _moreTipsCommand;

        public HomeViewModel()
        {
            IntakeWater = 300;
            TotalIntakeWater = 30000;
            SelectedQuantityWater = 300;
            ShowHomeTip();
        }

        private void ShowHomeTip()
        {
            int tipCounter = Convert.ToInt32(AppData.GetProfileData(AppDataKey.TipCounter));
            if (tipCounter< WaterGuideList.Count)
            {
                HomeTip = WaterGuideList[tipCounter].Tips;
            }
        }

        public string DisplayTotalIntakeWater
        {
            get { return "/" + TotalIntakeWater + " ml"; }
        }

        public string DisplaySelectedQuantityWater
        {
            get { return SelectedQuantityWater + " ml"; }
        }

        public string HomeTip
        {
            get { return _homeTip; }
            set
            {
                _homeTip = value;
                RaisePropertyChanged(() => HomeTip);
            }
        }

        public double IntakeWater
        {
            get { return _intakeWater; }
            set
            {
                _intakeWater = value;
                RaisePropertyChanged(() => IntakeWater);
            }
        }

        public double TotalIntakeWater
        {
            get => _totalIntakeWater;
            set => _totalIntakeWater = value;
        }

        public double SelectedQuantityWater
        {
            get => _selectedQuantityWater;
            set => _selectedQuantityWater = value;
        }

        //command

        public ICommand DrunkWaterCommand
        {
            get
            {
                _drunkWaterCommand = _drunkWaterCommand ?? new MvxCommand(AddDrunkWater);
                return _drunkWaterCommand;
            }
        }

        public ICommand MoreTipsCommand
        {
            get
            {
                _moreTipsCommand = _moreTipsCommand ?? new MvxAsyncCommand(ShowMoreTips);
                return _moreTipsCommand;
            }
        }

        private void AddDrunkWater()
        {
            if (IntakeWater < TotalIntakeWater)
            {
                IntakeWater += IntakeWater;
            }
            else
            {
                IntakeWater = TotalIntakeWater;
            }

            int tipCounter = Convert.ToInt32(AppData.GetProfileData(AppDataKey.TipCounter));
            if (tipCounter >= WaterGuideList.Count - 1)
            {
                AppData.SetProfileData(AppDataKey.TipCounter, null);
            }
            else
            {
                AppData.SetProfileData(AppDataKey.TipCounter, Convert.ToString(tipCounter + 1));
            }

            ShowHomeTip();
        }

        private async Task ShowMoreTips()
        {
            await NavigationService.Navigate<MoreTipsViewModel>();
        }
    }
}
