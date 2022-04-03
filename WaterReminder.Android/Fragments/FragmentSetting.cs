using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.Fragment.App;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Platforms.Android.Views.Fragments;
using WaterReminder.ViewModel;

namespace WaterReminder.Android.Fragments
{
    [MvxFragmentPresentation(ViewModelType = typeof(SettingViewModel))]
    public class FragmentSetting : MvxFragment<SettingViewModel>
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            // Create your fragment here
        }

        public static FragmentSetting NewInstance()
        {
            var frag2 = new FragmentSetting { Arguments = new Bundle() };
            return frag2;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);

            var view = this.BindingInflate(Resource.Layout.fragment_setting, container, false);

            return view;
        }
    }
}
