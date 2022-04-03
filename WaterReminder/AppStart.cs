using System.Threading.Tasks;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using WaterReminder.DataService;
using WaterReminder.ViewModel;

namespace WaterReminder
{
    public class AppStart : MvxAppStart
    {
        private readonly IMvxNavigationService _navigationService;

        public AppStart(IMvxApplication application, IMvxNavigationService navigationService) : base(application, navigationService)
        {
            _navigationService = navigationService;
            StartApp();
        }

        protected override Task NavigateToFirstViewModel(object hint = null)
        {
            if (AppData.GetIsProfileDataSaved())
            {
                return NavigationService.Navigate<MainViewModel>();
            }
            else
            {
                return NavigationService.Navigate<ProfileViewModel>();
            }
        }

        private async void StartApp()
        {
            if (AppData.GetIsProfileDataSaved())
            {
                await NavigationService.Navigate<MainViewModel>();
            }
            else
            {
                await NavigationService.Navigate<ProfileViewModel>();
            }
        }
    }
}
