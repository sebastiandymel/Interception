using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Autofac;

namespace Interception.Castle
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var cb = new ContainerBuilder();
            cb.RegisterAssemblyModules(typeof(App).Assembly);
            Container = cb.Build();
        }

        public IContainer Container { get; set; }


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var window = Container.Resolve<Window>();
            window.Show();
        }
    }
}
