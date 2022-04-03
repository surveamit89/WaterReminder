using System;
using System.Threading.Tasks;
using MvvmCross;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Essentials;

namespace WaterReminder.ViewModel
{
    public class BaseViewModel : MvxViewModel<bool> , IMvxViewModelResult<bool>
    {
        public static IMvxNavigationService NavigationService = Mvx.IoCProvider.Resolve<IMvxNavigationService>();

        public override void Prepare(bool parameter)
        {
            
        }
        public TaskCompletionSource<object> CloseCompletionSource { get; set; }

        public override void ViewDestroy(bool viewFinishing = true)
        {
            if (viewFinishing && CloseCompletionSource != null && !CloseCompletionSource.Task.IsCompleted && !CloseCompletionSource.Task.IsFaulted)
                CloseCompletionSource?.TrySetCanceled();

            base.ViewDestroy(viewFinishing);
        }
    }
}
