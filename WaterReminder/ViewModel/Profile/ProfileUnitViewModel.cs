using System.Collections.ObjectModel;
using System.Windows.Input;
using MvvmCross.Commands;
using WaterReminder.Model.Profile;

namespace WaterReminder.ViewModel.Profile
{
    public class ProfileUnitViewModel : BaseViewModel
    {
        private ObservableCollection<GenderData> _genders;
        private GenderData _selectedGender;
        private ICommand _closeCommand;
        private ICommand _genderSelectedCommand;

        public ProfileUnitViewModel()
        {
            Genders = new ObservableCollection<GenderData> { new GenderData { Gender = "Male" }, new GenderData { Gender = "Female" } };
        }


        public ObservableCollection<GenderData> Genders
        {
            get => _genders;
            set => _genders = value;
        }

        public GenderData SelectedGender
        {
            get => _selectedGender;
            set => _selectedGender = value;
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

        public ICommand GenderSelectedCommand
        {
            get
            {
                _genderSelectedCommand = _genderSelectedCommand ?? new MvxCommand(GenderSelected);
                return _genderSelectedCommand;
            }
        }

        private void ClosePage()
        {
            NavigationService.Close(this);
        }

        private void GenderSelected()
        {

        }
    }
}
