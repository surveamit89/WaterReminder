using Android.App;
using Android.OS;
using Android.Views;
using Google.Android.Material.BottomNavigation;
using WaterReminder.Android.Fragments;
using Fragment = AndroidX.Fragment.App.Fragment;
using WaterReminder.ViewModel;
using Android.Content.PM;
using Android.Content;
using Acr.UserDialogs;
using System;

namespace WaterReminder.Android
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", WindowSoftInputMode = SoftInput.StateHidden, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : BaseActivity<MainViewModel>, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        private bool doubleBackPressed = false;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.bottom_navigation);
            navigation.SetOnNavigationItemSelectedListener(this);
            LoadFragment(Resource.Id.navigation_home);
        }
        
        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.navigation_home:
                    LoadFragment(item.ItemId);
                    return true;
                case Resource.Id.navigation_history:
                    LoadFragment(item.ItemId);
                    return true;
                case Resource.Id.navigation_settings:
                    LoadFragment(item.ItemId);
                    return true;
            }
            return false;
        }

        void LoadFragment(int id)
        {
            Fragment fragment = null;
            switch (id)
            {
                case Resource.Id.navigation_home:
                    fragment = FragmentHome.NewInstance();
                    break;
                case Resource.Id.navigation_history:
                    fragment = FragmentHistory.NewInstance();
                    break;
                case Resource.Id.navigation_settings:
                    fragment = FragmentSetting.NewInstance();
                    break;
            }

            if (fragment == null)
                return;

            SupportFragmentManager.BeginTransaction()
                .Replace(Resource.Id.content_frame, fragment)
                .Commit();
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
                Message = Resources.GetString(Resource.String.exit_dialogue_title),
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

