using System;
using System.Windows.Input;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace WaterReminder.Model
{
    public class SwitchCupModel: MvxViewModel
    {
        public string DisplayCupSize { get; set; }
       
        public double CupSize { get; set; }

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

        public ICommand SelectCupCommand { get; set; }

        private ICommand _deleteCommand;

        public ICommand SelectCommand
        {
            get
            {
                return _deleteCommand ?? (_deleteCommand = new MvxCommand(ProcessSelectCommand));
            }
        }

        private void ProcessSelectCommand()
        {
            SelectCupCommand?.Execute(this);
        }
    }
}
