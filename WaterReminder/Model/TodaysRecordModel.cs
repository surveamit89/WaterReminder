using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MvvmCross.Commands;
using Newtonsoft.Json;
using WaterReminder.ViewModel;

namespace WaterReminder.Model
{
    public class TodaysRecordModel:BaseViewModel
    {
        public string ActivityTime { get; set; }
        public double Intake { get; set; }
        public string DisplayIntake { get; set; }

        [JsonIgnore]
        public ICommand DeleteRecordCommand { get; set; }

        [JsonIgnore]
        private ICommand _deleteCommand;

        [JsonIgnore]
        public ICommand DeleteCommand
        {
            get
            {
                return _deleteCommand ?? (_deleteCommand = new MvxCommand(ProcessDeleteCommandCommand));
            }
        }

        private void ProcessDeleteCommandCommand()
        {
            DeleteRecordCommand?.Execute(this);
        }
    }
}
