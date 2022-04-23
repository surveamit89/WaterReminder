using System;
using SkiaSharp;

namespace WaterReminder.Utility
{
    public static class Constant
    {
       public static readonly string IntakGoalFormat = "{0} ml";
       public static readonly int IntakGoalRecommendation = 3000;
       public static readonly string SelectedUnit = "kg , ml";
       public static readonly string WightFormat = "{0} kg";
       public static readonly int NotificationSericeID = 358;
       public static readonly string MLDayFormat = "{0} ml/day";
       public static readonly string TimesDayFormat = "{0} times/day";

        //color
        public static readonly SKColor RedColor = SKColor.Parse("#FF1943");
        public static readonly SKColor GreenColor = SKColor.Parse("#00CED1");
        public static readonly SKColor BlueColor = SKColor.Parse("#00BFFF");
        public static readonly SKColor BlackColor = SKColor.Parse("#000000");
        public static readonly SKColor AppColor = SKColor.Parse("#3498db");
    }
}
