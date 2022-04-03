using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MvvmCross.Commands;
using WaterReminder.DataService;
using WaterReminder.Model.Profile;

namespace WaterReminder.ViewModel.Profile
{
    public class ProfileGenderViewModel : BaseViewModel
    {
        //private ObservableCollection<GenderData> _genders;
        //private GenderData _selectedGender;
        private bool _isMaleSelected;
        private bool _isFemaleSelected;
        private ICommand _closeCommand;
        private ICommand _genderSelectedCommand;

        public ProfileGenderViewModel()
        {
            if (!string.IsNullOrEmpty(AppData.GetProfileData(AppDataKey.ProfileGender)))
            {
                if (AppData.GetProfileData(AppDataKey.ProfileGender)=="Male")
                {
                    IsMaleSelected = true;
                    IsFemaleSelected = false;
                }
                else
                {
                    IsMaleSelected = false;
                    IsFemaleSelected = true;
                }
            }
            //Genders = new ObservableCollection<GenderData> { new GenderData { Gender = "Male" }, new GenderData { Gender = "Female" } };
        }


        //public ObservableCollection<GenderData> Genders
        //{
        //    get => _genders;
        //    set => _genders = value;
        //}

        public bool IsMaleSelected
        {
            get { return _isMaleSelected; }
            set
            {
                _isMaleSelected = value;
                RaisePropertyChanged(() => IsMaleSelected);
            }
        }

        public bool IsFemaleSelected
        {
            get { return _isFemaleSelected; }
            set
            {
                _isFemaleSelected = value;
                RaisePropertyChanged(() => IsFemaleSelected);
            }
        }

        //public GenderData SelectedGender
        //{
        //    get => _selectedGender;
        //    set => _selectedGender = value;
        //}

        //command
        public ICommand CloseCommand
        {
            get
            {
                _closeCommand = _closeCommand ?? new MvxCommand<bool>(ClosePage);
                return _closeCommand;
            }
        }

        public ICommand GenderSelectedCommand
        {
            get
            {
                _genderSelectedCommand = _genderSelectedCommand ?? new MvxCommand<string>(GenderSelected);
                return _genderSelectedCommand;
            }
        }

        private void GenderSelected(string selectedMale)
        {
            if (selectedMale=="Male")
            {
                IsMaleSelected = true;
                IsFemaleSelected = false;
            }
            else
            {
                IsMaleSelected = false;
                IsFemaleSelected = true;
            }
        }

        private void ClosePage(bool isDataUpdated)
        {
            if (isDataUpdated)
            {
                string seLectedGender = IsMaleSelected ? "Male" : "Female";
                AppData.SetProfileData(AppDataKey.ProfileGender, seLectedGender);
            }
            NavigationService.Close(this, isDataUpdated);
        }
    }
}
