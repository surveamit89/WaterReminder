using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microcharts;
using Newtonsoft.Json;
using SkiaSharp;
using WaterReminder.DataService;
using WaterReminder.Model;
using WaterReminder.Utility;

namespace WaterReminder.ViewModel
{
    public class HistoryViewModel : BaseViewModel
    {
        public HistoryViewModel()
        {
            DisplayAllRecord();
            AvgWeekly = string.Format(Constant.MLDayFormat, 3000);
            AvgMonthly = string.Format(Constant.MLDayFormat, 2400);
            AvgCompletion = "20%";
            DrinkFrequency = string.Format(Constant.TimesDayFormat, 4);
            //ShowDummyData();
        }

        public ObservableCollection<ChartEntry> Entries { get; set; }

        public string AvgWeekly { get; set; }

        public string AvgMonthly { get; set; }

        public string AvgCompletion { get; set; }

        public string DrinkFrequency { get; set; }

        private ObservableCollection<SelectedDateRecordModel> _allRecords;
        public ObservableCollection<SelectedDateRecordModel> AllRecords
        {
            get { return _allRecords; }
            set
            {
                _allRecords = value;
                RaisePropertyChanged(() => AllRecords);
            }
        }

        private void DisplayAllRecord()
        {
            string jsonData = AppData.GetData(AppDataKey.AllRecords);
            if (string.IsNullOrEmpty(jsonData))
            {
                AllRecords = new ObservableCollection<SelectedDateRecordModel>();
            }
            else
            {
                AllRecords = new ObservableCollection<SelectedDateRecordModel>();

                var records = JsonConvert.DeserializeObject<IList<SelectedDateRecordModel>>(jsonData);
                AllRecords = new ObservableCollection<SelectedDateRecordModel>(records.OrderByDescending(a=>a.ActivityDate));
            }
            if (AllRecords!=null && AllRecords.Count()>0)
            {
                DisplayChart();
            }
            
        }

        private void DisplayChart()
        {
            Entries = new ObservableCollection<ChartEntry>();
            for (int i = 0; i < 7; i++)
            {
                if (AllRecords.Count>i && AllRecords[i].TotalIntakeTaken > 0)
                {
                    Entries.Add(new ChartEntry(Convert.ToSingle(AllRecords[i].TotalIntakeTaken))
                    {
                        Color = GetColor(AllRecords[i].TotalIntakeTaken),
                        Label = AllRecords[i].ActivityDate,
                        ValueLabel = string.Format(Constant.IntakGoalFormat, AllRecords[i].TotalIntakeTaken.ToString()),
                        TextColor = Constant.BlackColor,
                        ValueLabelColor = Constant.AppColor
                    });
                }
                else
                {
                    Entries.Add(new ChartEntry(0)
                    {
                        Color = GetColor(0),
                        Label = DateTime.Now.AddDays(-i).ToString("dd/m/yyyy"),
                        ValueLabel = string.Format(Constant.IntakGoalFormat, 0),
                        TextColor = Constant.BlackColor,
                        ValueLabelColor = Constant.AppColor
                    });
                }
            }
        }

        private SKColor GetColor(double totalIntakeTaken)
        {
            if (totalIntakeTaken>2000)
            {
                return Constant.GreenColor;
            }
            else if(totalIntakeTaken > 1000)
            {
                return Constant.BlueColor;
            }
            else
            {
                return Constant.RedColor;
            }
        }

        private void ShowDummyData()
        {
            Entries = new ObservableCollection<ChartEntry>();
            for (int i = 0; i < 7; i++)
            {
                Entries.Add(new ChartEntry(Convert.ToSingle(i*400))
                {
                    Color = GetColor(i * 400),
                    Label = DateTime.Now.AddDays(-i).ToString("dd/M/yyyy"),
                    ValueLabel = string.Format(Constant.IntakGoalFormat, (i * 400).ToString()),
                    TextColor = Constant.BlackColor,
                    ValueLabelColor = Constant.AppColor
                });
            }
        }
    }
}
