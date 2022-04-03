using Android.OS;
using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Platforms.Android.Views.Fragments;
using WaterReminder.Android.Fragments;
using WaterReminder.ViewModel.Profile;

namespace WaterReminder.Android.Activity.Profile
{
    [MvxFragmentPresentation(ViewModelType = typeof(ProfileIntakeGoalViewModel))]
    public class ProfileIntakeGoalDialogueActivity : MvxDialogFragment<ProfileIntakeGoalViewModel>
    {
        SeekBar seekbar;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ViewModel.RefreshSelectedValue += (int value) =>
            {
                seekbar.Progress = value;
            };
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

            var view = this.BindingInflate(Resource.Layout.dialogue_intake_goal, container, false);
            seekbar = view.FindViewById<SeekBar>(Resource.Id.seekbar);
            seekbar.Progress = ViewModel.SelectedValue;
            TextView textView = view.FindViewById<TextView>(Resource.Id.seekbar_value);
            seekbar.ProgressChanged+=(object sender, SeekBar.ProgressChangedEventArgs e) => {
                if (e.FromUser)
                {
                    ViewModel.SelectedValue = e.Progress;
                }
            };
            return view;
        }
    }
}
