using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MvvmCross.Commands;
using Newtonsoft.Json;
using WaterReminder.ViewModel;

namespace WaterReminder.Model
{
    public class SelectedDateRecordModel
    {
        public string ActivityDate { get; set; }
        public double TotalIntakeTaken { get; set; }
        public ObservableCollection<TodaysRecordModel> Records { get; set; }

    }
    public class TodaysRecordModel:BaseViewModel
    {
        public string ActivityDate { get; set; }
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
                return _deleteCommand ?? (_deleteCommand = new MvxCommand(ProcessDeleteCommand));
            }
        }

        private void ProcessDeleteCommand()
        {
            DeleteRecordCommand?.Execute(this);
        }
    }
}
