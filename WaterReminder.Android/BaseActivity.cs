using System;
using System.Runtime.Remoting.Contexts;
using Acr.UserDialogs;
using Android.OS;
using Android.Widget;
using MvvmCross.Platforms.Android.Views;
using MvvmCross.ViewModels;
using Xamarin.Essentials;

namespace WaterReminder.Android
{
    public class BaseActivity<T> : MvxActivity<T> where T : MvxViewModel
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Platform.Init(this, bundle);
            UserDialogs.Init(this);
        }

        public Toolbar Toolbar
        {
            get;
            set;
        }

        protected int ActionBarIcon
        {
            set { Toolbar?.SetNavigationIcon(value); }
        }

    }
}
