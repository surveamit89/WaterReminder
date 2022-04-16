using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MvvmCross.Commands;
using WaterReminder.Model.MoreTips;

namespace WaterReminder.ViewModel
{
    public class MoreTipsViewModel : BaseViewModel
    {
        private ICommand _closeCommand;

        public string PageTitle => "How to drink water correctly?";

        public ICommand CloseCommand
        {
            get
            {
                _closeCommand = _closeCommand ?? new MvxCommand(ProcessCloseCommand);
                return _closeCommand;
            }
        }

        private void ProcessCloseCommand()
        {
            NavigationService.Close(this);
        }

    }
}
