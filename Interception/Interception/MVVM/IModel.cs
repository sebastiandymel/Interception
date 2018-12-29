using System.Threading.Tasks;

namespace SEDYInterception
{
    public interface IModel
    {
        [DoSomethingExtra]
        double Add(double x, double y);

        Task<double> Calculate(double x);
    }
}