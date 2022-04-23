using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Android.OS;
using Android.Views;
using Microcharts;
using Microcharts.Droid;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Platforms.Android.Views.Fragments;
using SkiaSharp;
using WaterReminder.ViewModel;

namespace WaterReminder.Android.Fragments
{
    [MvxFragmentPresentation(ViewModelType = typeof(HistoryViewModel))]
    public class FragmentHistory : MvxFragment<HistoryViewModel>
    {
        ChartView chartView;
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

            var view = this.BindingInflate(Resource.Layout.fragment_history, container, false);
            chartView = view.FindViewById<ChartView>(Resource.Id.chartView);
            //RadialGaugeChart,LineChart,DonutChart,PointChart,RadarChart,BarChart
            var chart = new BarChart() { Entries = ViewModel.Entries };
            chartView.Chart = chart;

            return view;
        }

    }
}
