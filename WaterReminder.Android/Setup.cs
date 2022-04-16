using System.Collections.Generic;
using System.Reflection;
using AndroidX.RecyclerView.Widget;
using MvvmCross;
using MvvmCross.Platforms.Android.Core;
using MvvmCross.Platforms.Android.Presenters;
using MvvmCross.ViewModels;
using WaterReminder.DataService;

namespace WaterReminder.Android
{
    public class Setup : MvxAndroidSetup
    {
        protected override IMvxApplication CreateApp()
        {
            return new App();
        }

        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();
            Mvx.IoCProvider.RegisterSingleton<IStartService>(new StartServiceAndroid());
        }

        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            var mvxFragmentsPresenter = new MvxAndroidViewPresenter(AndroidViewAssemblies);
            Mvx.IoCProvider.RegisterSingleton<IMvxAndroidViewPresenter>(mvxFragmentsPresenter);
            return mvxFragmentsPresenter;
        }

        protected override IEnumerable<Assembly> AndroidViewAssemblies => new List<Assembly>(base.AndroidViewAssemblies)
        {
            typeof(RecyclerView).Assembly
        };

        public override Assembly ExecutableAssembly => base.ExecutableAssembly;

        protected override void FillValueConverters(MvvmCross.Converters.IMvxValueConverterRegistry registry)
        {
            base.FillValueConverters(registry);
            registry.AddOrOverwrite("Language", new MvvmCross.Localization.MvxLanguageConverter());
        }

        protected override void InitializeLastChance()
        {
            base.InitializeLastChance();
        }
    }
}
