using System.Threading.Tasks;

namespace SEDYInterception
{
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
}