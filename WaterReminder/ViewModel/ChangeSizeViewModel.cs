using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using MvvmCross.Commands;
using WaterReminder.DataService;
using WaterReminder.Model;

namespace WaterReminder.ViewModel
{
    public class ChangeSizeViewModel:BaseViewModel
    {
        private ICommand _closeCommand;
        private ICommand _selectCupCommand;
        

        public string PageTitle => "Switch cup?";

        public ObservableCollection<SwitchCupModel> CupSizeList { get; set; }

        public ChangeSizeViewModel()
        {
            CupSizeList = new ObservableCollection<SwitchCupModel> {
                new SwitchCupModel { DisplayCupSize = "100 ml" ,CupSize=100, SelectCupCommand=SelectCupCommand },
                new SwitchCupModel { DisplayCupSize = "125 ml" ,CupSize=125, SelectCupCommand=SelectCupCommand },
                new SwitchCupModel { DisplayCupSize = "150 ml" ,CupSize=150, SelectCupCommand=SelectCupCommand },
                new SwitchCupModel { DisplayCupSize = "175 ml" ,CupSize=175, SelectCupCommand=SelectCupCommand },
                new SwitchCupModel { DisplayCupSize = "200 ml" ,CupSize=200, SelectCupCommand=SelectCupCommand },
                new SwitchCupModel { DisplayCupSize = "300 ml" ,CupSize=300, SelectCupCommand=SelectCupCommand },
                new SwitchCupModel { DisplayCupSize = "400 ml" ,CupSize=400, SelectCupCommand=SelectCupCommand }
            };
            CheckCupSize();
        }

        private void CheckCupSize()
        {
            double cupSize=Convert.ToDouble(AppData.GetData(AppDataKey.IntakeCupSize));
            if (cupSize==0)
            {
                CupSizeList[CupSizeList.Count - 1].IsSelected = true;
            }
            else
            {
                var found = CupSizeList.FirstOrDefault(a => a.CupSize == cupSize);
                if (found!=null)
                {
                    ProcessSelectCupCommand(found);
                }
            }
        }

        public ICommand SelectCupCommand
        {
            get
            {
                return _selectCupCommand ?? (_selectCupCommand = new MvxCommand<SwitchCupModel>(ProcessSelectCupCommand));
            }
        }

        private void ProcessSelectCupCommand(SwitchCupModel selectedCup)
        {
            foreach (var item in CupSizeList)
            {
                item.IsSelected = item.CupSize == selectedCup.CupSize ? true : false;
            }
            AppData.SetData(AppDataKey.IntakeCupSize, selectedCup.CupSize.ToString());
        }

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
