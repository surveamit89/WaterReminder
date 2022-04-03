using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Commands;
using WaterReminder.Model.Profile;
using WaterReminder.ViewModel.Profile;
using Xamarin.Essentials;

namespace WaterReminder.ViewModel
{
    public class SettingViewModel: ProfileMainViewModel
    {
        public string VersionName
        {
            get { return "Version "+ VersionTracking.CurrentVersion + "." + VersionTracking.CurrentBuild; }
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();
            AppProfileData = new ProfileData();
            LoadProfileData();
        }

        private ICommand _editCommand;

        public ICommand EditCommand
        {
            get
            {
                _editCommand = _editCommand ?? new MvxAsyncCommand(EditPage);
                return _editCommand;
            }
        }

        private async Task EditPage()
        {
            await NavigationService.Navigate<ProfileViewModel>();
        }

    }
}
