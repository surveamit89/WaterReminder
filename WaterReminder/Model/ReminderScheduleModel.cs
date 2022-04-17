using System;
using System.Windows.Input;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace WaterReminder.Model
{
    public class ReminderScheduleModel : MvxViewModel
    {
        public string DisplayMinutes{ get; set; }

        public int SelectedMinutes { get; set; }

        private bool _isSelected { get; set; }
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                RaisePropertyChanged(() => IsSelected);
            }
        }

        public ICommand SelectScheduleCommand { get; set; }

        private ICommand _selectCommand;

        public ICommand SelectCommand
        {
            get
            {
                return _selectCommand ?? (_selectCommand = new MvxCommand(ProcessSelectCommand));
            }
        }

        private void ProcessSelectCommand()
        {
            SelectScheduleCommand?.Execute(this);
        }
    }
}
