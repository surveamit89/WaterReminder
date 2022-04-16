using System.Collections.ObjectModel;
using System.Windows.Input;
using MvvmCross.Commands;
using WaterReminder.DataService;
using WaterReminder.Model.Profile;
using WaterReminder.Utility;

namespace WaterReminder.ViewModel.Profile
{
    public class ProfileWeightViewModel : BaseViewModel
    {
        private ObservableCollection<WeightData> _weightList;
        private WeightData _selectedWeight;
        private ICommand _closeCommand;
        private ICommand _genderSelectedCommand;

        public ProfileWeightViewModel()
        {
            WeightList = new ObservableCollection<WeightData>();
            
            for (int i = 1; i <= 400; i++)
            {
                WeightList.Add(new WeightData { Weight = i, DisplayData = string.Format(Constant.WightFormat, i) });
            }
        }


        public ObservableCollection<WeightData> WeightList
        {
            get => _weightList;
            set => _weightList = value;
        }

        //public WeightData SelectedWeight
        //{
        //    get { return _selectedWeight; }
        //    set
        //    {
        //        _selectedWeight = value;
        //        RaisePropertyChanged(() => SelectedWeight);
        //    }
        //}

        public WeightData SelectedWeight
        {
            get => _selectedWeight;
            set => _selectedWeight = value;
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
            if (isDataUpdated)
            {
                AppData.SetData(AppDataKey.ProfileWeight, SelectedWeight.Weight.ToString());
            }
            NavigationService.Close(this, isDataUpdated);
        }
    }
}
