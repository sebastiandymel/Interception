using System.Diagnostics;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace SEDYInterception
{
    public class ViewModel2 : ViewModelBase 
    {
        private readonly IModel model;
        private string result;

        public ViewModel2(IModel model)
        {
            this.model = model;
            ExecuteCommand = new RelayCommand(Execute);
        }

        public RelayCommand ExecuteCommand { get; set; }

        private async void Execute()
        {
            var x = 100;
            var y = 100;
            Result = $"{x} + {y} = " + this.model.Add(100, 100).ToString("####");


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
    }
}