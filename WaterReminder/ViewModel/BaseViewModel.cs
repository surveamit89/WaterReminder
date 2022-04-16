using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MvvmCross;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using WaterReminder.Model.MoreTips;
using Xamarin.Essentials;

namespace WaterReminder.ViewModel
{
    public class BaseViewModel : MvxViewModel<bool> , IMvxViewModelResult<bool>
    {
        public static IMvxNavigationService NavigationService = Mvx.IoCProvider.Resolve<IMvxNavigationService>();

        public ObservableCollection<MoreTipsModel> WaterGuideList { get; set; }

        public BaseViewModel()
        {
            WaterGuideList = new ObservableCollection<MoreTipsModel> {
                new MoreTipsModel { Tips = "Drink your glass of water slowly with some small sips" },
                new MoreTipsModel{ Tips = "Hold the water in your mouth for a while before swallowing" },
                new MoreTipsModel { Tips = "Drinking water in sitting posture is better than in a standing or running position" },
                new MoreTipsModel{ Tips = "Do not drink cold water or water with ice" },
                new MoreTipsModel { Tips = "Do not drink water immediately after eating" },
                new MoreTipsModel{ Tips = "Do not drink cold water immediately after hot drinks like tea or coffee" },
                new MoreTipsModel { Tips = "Always drink water before urinating and do not drink water immediately after urinating" }
            }
            ;
        }

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
