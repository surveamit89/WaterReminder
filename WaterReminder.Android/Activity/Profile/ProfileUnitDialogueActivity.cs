using Android.OS;
using Android.Views;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Platforms.Android.Views.Fragments;
using WaterReminder.Android.Fragments;
using WaterReminder.ViewModel.Profile;

namespace WaterReminder.Android.Activity.Profile
{
    [MvxFragmentPresentation(ViewModelType = typeof(ProfileUnitViewModel))]
    public class ProfileUnitDialogueActivity : MvxDialogFragment<ProfileUnitViewModel>
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public static FragmentHistory NewInstance()
        {
            var frag2 = new FragmentHistory { Arguments = new Bundle() };
            return frag2;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);

            var view = this.BindingInflate(Resource.Layout.dialogue_unit, container, false);

            return view;
        }
    }
}
