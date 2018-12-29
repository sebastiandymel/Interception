using System;
using System.Diagnostics;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Interception.Castle
{
    public class ViewModel : ViewModelBase
    {
        private readonly IModel model;
        private string result;
        private string x;
        private string y;

        public ViewModel(IModel model)
        {
            this.model = model;
            ExecuteCommand = new RelayCommand(Execute);
        }

        public RelayCommand ExecuteCommand { get; set; }

        private async void Execute()
        {
            double.TryParse(X, out var xVal);
            double.TryParse(Y, out var yVal);
            Result = this.model.Add(xVal, yVal).ToString("####");
            

            var sum = await this.model.Calculate(100);
            Debug.WriteLine($"sum is {sum}");
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

        public string X
        {
            get { return this.x; }
            set
            {
                this.x = value;
                RaisePropertyChanged();
            }
        }

        public string Y
        {
            get { return this.y; }
            set
            {
                this.y = value;
                RaisePropertyChanged();
            }
        }
    }

    public interface IModel
    {
        [DoSomethingExtra]
        double Add(double x, double y);

        Task<double> Calculate(double x);
    }

    public class Model : IModel
    {
        public double Add(double x, double y)
        {
            return x + y;
        }

        public async Task<double> Calculate(double x)
        {
            await Task.Delay(1000);

            return x * 100;
        }
    }




    [AttributeUsage(AttributeTargets.Method)]
    public class DoSomethingExtraAttribute : Attribute
    {

    }
}