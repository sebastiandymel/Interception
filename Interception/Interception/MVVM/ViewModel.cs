using System;
using System.Threading;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace SEDYInterception
{
    public class ViewModel : ViewModelBase 
    {
        private readonly IHeavyCalculationModel model;
        private string result;
        private int index = 1;

        public ViewModel(IHeavyCalculationModel model)
        {
            this.model = model;
            this.model.Calculated += OnCalculated;
            ExecuteCommand = new RelayCommand(Execute);
        }

        private void OnCalculated(object sender, EventArgs e)
        {
            Result += index++ + " ";
        }

        public RelayCommand ExecuteCommand { get; set; }

        private  void Execute()
        {
            this.model.Calculate();
        }

        public string Result
        {
            get { return this.result; }
            set
            {
                this.result = value;
                RaisePropertyChanged();
            }
        }
    }

    public interface IHeavyCalculationModel
    {
        void Calculate();
        event EventHandler Calculated;
    }

    public class HeavyCalculator : IHeavyCalculationModel
    {
        public event EventHandler Calculated;

        public void Calculate()
        {
            Thread.Sleep(2000);
            Calculated?.Invoke(this, EventArgs.Empty);
        }
    }
}