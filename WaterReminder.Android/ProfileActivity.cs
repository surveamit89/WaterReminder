using System;
using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using WaterReminder.ViewModel;

namespace WaterReminder.Android
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", WindowSoftInputMode = SoftInput.StateHidden, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class ProfileActivity : BaseActivity<ProfileViewModel>
    {
        private bool doubleBackPressed = false;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_profile);
        }

        public override void OnBackPressed()
        {
            if (doubleBackPressed)
            {
                ExitApp();
                return;
            }

            this.doubleBackPressed = true;

            Handler h = new Handler(Looper.MainLooper);
            Action myAction = () =>
            {
                doubleBackPressed = false;
            };

            h.PostDelayed(myAction, 2000);
        }

        private async void ExitApp()
        {
            var result = await UserDialogs.Instance.ConfirmAsync(new ConfirmConfig
            {
                Message =Resources.GetString(Resource.String.exit_dialogue_title),
                OkText = Resources.GetString(Resource.String.title_yes),
                CancelText = Resources.GetString(Resource.String.title_cancel)
            });
            if (result)
            {
                var a = new Intent(Intent.ActionMain);
                a.AddCategory(Intent.CategoryHome);
                a.SetFlags(ActivityFlags.NewTask);
                StartActivity(a);
            }
        }
    }
}
