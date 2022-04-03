using System;
using System.Windows.Input;
using MvvmCross.Binding.ValueConverters;
using MvvmCross.Converters;
using WaterReminder.Model.Profile;

namespace WaterReminder.Converter
{
    public class OpenDialogueCommandValueConverter
    : MvxValueConverter<ICommand, ICommand>
    {
        protected override ICommand Convert(ICommand value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new MvxWrappingCommand(value, Enum.Parse(typeof(ProfileDialogueEnum), parameter.ToString()));
        }
    }
}
