using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using WaterReminder.ViewModel;

namespace WaterReminder.Android
{
    [Activity(Theme = "@style/SplashTheme", MainLauncher = true, WindowSoftInputMode = SoftInput.StateHidden, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashActivity : BaseActivity<BaseViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
    }
}