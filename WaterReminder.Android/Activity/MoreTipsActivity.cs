using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using WaterReminder.ViewModel;

namespace WaterReminder.Android.Activity
{
    [Activity(Theme = "@style/AppTheme", WindowSoftInputMode = SoftInput.StateHidden, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MoreTipsActivity:BaseActivity<MoreTipsViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_more_tips);
            // Create your application here
        }
    }
}
