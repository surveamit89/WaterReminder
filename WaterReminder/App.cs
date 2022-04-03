using System;
using System.Threading.Tasks;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;

namespace WaterReminder
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            Mvx.IoCProvider.ConstructAndRegisterSingleton<IMvxAppStart, AppStart>();
        }
        public override Task Startup()
        {
            return base.Startup();
        }
    }
}
