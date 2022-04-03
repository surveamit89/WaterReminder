using System;
using System.Windows.Input;
using MvvmCross.Commands;

namespace WaterReminder.ViewModel
{
    public class MoreTipsViewModel : BaseViewModel
    {
        private ICommand _closeCommand;

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
