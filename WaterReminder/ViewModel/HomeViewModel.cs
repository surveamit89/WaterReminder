using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Commands;
using WaterReminder.DataService;
using WaterReminder.Model;
using Acr.UserDialogs;
using Newtonsoft.Json;
using System.Collections.Generic;
using WaterReminder.Utility;
using MvvmCross;

namespace WaterReminder.ViewModel
{
    public class HomeViewModel : BaseViewModel
    {
        //Property
        private double _intakeWater;
        private double _totalIntakeWater;
        private double _selectedQuantityWater;
        private string _homeTip;
        private ICommand _drunkWaterCommand;
        private ICommand _moreTipsCommand;
        private ICommand _deleteRecordCommand;
        private ICommand _changeIntakeCommand;
        
        public override void ViewAppearing()
        {
            IntakeWater = Convert.ToDouble(AppData.GetData(AppDataKey.ProfileIntakeTaken));

            TotalIntakeWater = Convert.ToDouble(AppData.GetData(AppDataKey.ProfileIntakeGoal));
            if (TotalIntakeWater == 0)
            {
                TotalIntakeWater = 3000;
            }
            DisplayTotalIntakeWater= "/"+string.Format(Constant.IntakGoalFormat, TotalIntakeWater);

            SelectedQuantityWater = Convert.ToDouble(AppData.GetData(AppDataKey.IntakeCupSize));
            if (SelectedQuantityWater == 0)
            {
                SelectedQuantityWater = 300;
                AppData.SetData(AppDataKey.IntakeCupSize, SelectedQuantityWater.ToString());
            }
            DisplaySelectedQuantityWater = string.Format(Constant.IntakGoalFormat, SelectedQuantityWater);
            ShowHomeTip();
            DisplayTodaysRecord();
        }

        private void ShowHomeTip()
        {
            int tipCounter = Convert.ToInt32(AppData.GetData(AppDataKey.TipCounter));
            if (tipCounter < WaterGuideList.Count)
            {
                HomeTip = WaterGuideList[tipCounter].Tips;
            }
        }

        private string _displayTotalIntakeWater;
        public string DisplayTotalIntakeWater
        {
            get { return _displayTotalIntakeWater; }
            set
            {
                _displayTotalIntakeWater = value;
                RaisePropertyChanged(() => DisplayTotalIntakeWater);
            }
        }

        private string _displaySelectedQuantityWater;
        public string DisplaySelectedQuantityWater
        {
            get { return _displaySelectedQuantityWater; }
            set
            {
                _displaySelectedQuantityWater = value;
                RaisePropertyChanged(() => DisplaySelectedQuantityWater);
            }
        }

        public string HomeTip
        {
            get { return _homeTip; }
            set
            {
                _homeTip = value;
                RaisePropertyChanged(() => HomeTip);
            }
        }

        public double IntakeWater
        {
            get { return _intakeWater; }
            set
            {
                _intakeWater = value;
                RaisePropertyChanged(() => IntakeWater);
            }
        }

        public double TotalIntakeWater
        {
            get => _totalIntakeWater;
            set => _totalIntakeWater = value;
        }

        public double SelectedQuantityWater
        {
            get => _selectedQuantityWater;
            set => _selectedQuantityWater = value;
        }

        private ObservableCollection<TodaysRecordModel> _todaysRecords;
        public ObservableCollection<TodaysRecordModel> TodaysRecords
        {
            get { return _todaysRecords; }
            set
            {
                _todaysRecords = value;
                RaisePropertyChanged(() => TodaysRecords);
            }
        }

        //command


        public ICommand DeleteRecordCommand
        {
            get
            {
                return _deleteRecordCommand ?? (_deleteRecordCommand = new MvxCommand<TodaysRecordModel>(ProcessDeleteRecordCommand));
            }
        }

        public ICommand DrunkWaterCommand
        {
            get
            {
                _drunkWaterCommand = _drunkWaterCommand ?? new MvxCommand(AddDrunkWater);
                return _drunkWaterCommand;
            }
        }

        public ICommand MoreTipsCommand
        {
            get
            {
                _moreTipsCommand = _moreTipsCommand ?? new MvxAsyncCommand(ShowMoreTips);
                return _moreTipsCommand;
            }
        }

        public ICommand ChangeIntakeCommand
        {
            get
            {
                _changeIntakeCommand = _changeIntakeCommand ?? new MvxAsyncCommand(ChangeIntake);
                return _changeIntakeCommand;
            }
        }

        private async Task ShowMoreTips()
        {
            await NavigationService.Navigate<MoreTipsViewModel>();
        }

        private async Task ChangeIntake()
        {
            await NavigationService.Navigate<ChangeSizeViewModel>();
        }

        private void AddDrunkWater()
        {
            CalculateIntakeWater();
            AddTodaysRecord();
            DisplayHomeTipData();
        }

        private void CalculateIntakeWater()
        {
            //if (IntakeWater <= TotalIntakeWater)
            //{
            //    IntakeWater += SelectedQuantityWater;
            //    AddTodaysRecord();
            //}
            //else
            //{
            //    IntakeWater = TotalIntakeWater;
            //}
            IntakeWater += SelectedQuantityWater;
            SaveIntakeData(IntakeWater);
        }

        private void DisplayHomeTipData()
        {
            int tipCounter = Convert.ToInt32(AppData.GetData(AppDataKey.TipCounter));
            if (tipCounter >= WaterGuideList.Count - 1)
            {
                AppData.SetData(AppDataKey.TipCounter, null);
            }
            else
            {
                AppData.SetData(AppDataKey.TipCounter, Convert.ToString(tipCounter + 1));
            }

            ShowHomeTip();
        }
        private void AddTodaysRecord()
        {
            TodaysRecords.Add(
                new TodaysRecordModel
                {
                    ActivityTime = DateTime.Now.ToString("hh:mm tt"),
                    Intake = SelectedQuantityWater,
                    DisplayIntake = DisplaySelectedQuantityWater,
                    DeleteRecordCommand = DeleteRecordCommand
                });
            TodaysRecords=new ObservableCollection<TodaysRecordModel>(TodaysRecords.Reverse());
            SaveRecordsData(TodaysRecords);
        }

        private void DisplayTodaysRecord()
        {
            string jsonData = AppData.GetData(AppDataKey.TodaysRecords);
            if (string.IsNullOrEmpty(jsonData))
            {
                TodaysRecords = new ObservableCollection<TodaysRecordModel>();
            }
            else
            {
                TodaysRecords = new ObservableCollection<TodaysRecordModel>();
                var records = JsonConvert.DeserializeObject<IList<TodaysRecordModel>>(jsonData);
                foreach (var item in records)
                {
                    TodaysRecords.Add(new TodaysRecordModel
                    {
                        ActivityTime = item.ActivityTime,
                        Intake = item.Intake,
                        DisplayIntake = item.DisplayIntake,
                        DeleteRecordCommand = DeleteRecordCommand
                    });
                }
                TodaysRecords = new ObservableCollection<TodaysRecordModel>(TodaysRecords.Reverse());
            }

        }
        private async void ProcessDeleteRecordCommand(TodaysRecordModel record)
        {
            var result = await UserDialogs.Instance.ConfirmAsync(new ConfirmConfig
            {
                Message = "Do you want to remove this record ?",
                OkText = "Yes",
                CancelText = "No"
            });
            if (result)
            {
                if (TodaysRecords.Any(a => a.ActivityTime == record.ActivityTime))
                {
                    var item = TodaysRecords.FirstOrDefault(a => a.ActivityTime == record.ActivityTime && a.Intake == record.Intake);
                    if (item != null)
                    {
                        TodaysRecords.Remove(item);
                        IntakeWater -= item.Intake;
                        SaveIntakeData(IntakeWater);
                        SaveRecordsData(TodaysRecords);
                    }
                }
            }
        }
        private void SaveRecordsData(ObservableCollection<TodaysRecordModel> todaysRecords)
        {
            string jsonData = JsonConvert.SerializeObject(todaysRecords);
            if (!string.IsNullOrEmpty(jsonData))
            {
                AppData.SetData(AppDataKey.TodaysRecords, jsonData);
            }
        }

        private void SaveIntakeData(double intakeWater)
        {
            AppData.SetData(AppDataKey.ProfileIntakeTaken, Convert.ToString(intakeWater));
        }
    }
}
