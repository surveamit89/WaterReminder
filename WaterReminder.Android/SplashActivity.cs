using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using WaterReminder.DataService;
using WaterReminder.ViewModel;

namespace WaterReminder.Android
{
    [Activity(Theme = "@style/SplashTheme", MainLauncher = true, WindowSoftInputMode = SoftInput.StateHidden, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashActivity : BaseActivity<BaseViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (AppData.GetIsProfileDataSaved())
            {
                var newIntent = new Intent(this, typeof(MainActivity));
                //newIntent.AddFlags(ActivityFlags.ClearTop);
                //newIntent.AddFlags(ActivityFlags.SingleTop);
                StartActivity(newIntent);
            }
            else
            {
                var newIntent = new Intent(this, typeof(ProfileActivity));
                //newIntent.AddFlags(ActivityFlags.ClearTop);
                //newIntent.AddFlags(ActivityFlags.SingleTop);
                StartActivity(newIntent);
            }
        }
    }
}