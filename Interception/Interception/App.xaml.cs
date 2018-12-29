using System.Windows;
using Unity;
using Unity.Interception.ContainerIntegration;

namespace SEDYInterception
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly UnityContainer container;

        public App()
        {
            this.container = new UnityContainer();
            this.container.AddNewExtension<Unity.Interception.ContainerIntegration.Interception>();
            this.container.AddNewExtension<CompositionModule>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);


            var window = this.container.Resolve<Window>("Main");
            window.Show();
        }
    }
}
