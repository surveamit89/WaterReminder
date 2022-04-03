using Android.OS;
using Android.Views;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Platforms.Android.Views.Fragments;
using WaterReminder.ViewModel;

namespace WaterReminder.Android.Fragments
{
    [MvxFragmentPresentation(ViewModelType = typeof(SettingViewModel))]
    public class FragmentHome : MvxFragment<HomeViewModel>
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public static FragmentHome NewInstance()
        {
            var frag1 = new FragmentHome { Arguments = new Bundle() };
            return frag1;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);

            var view = this.BindingInflate(Resource.Layout.fragment_home, container, false);

            return view;
        }

    }
}
