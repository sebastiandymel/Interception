using System.Windows;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Module = Autofac.Module;

namespace Interception.Castle
{
    public class CompositionModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<MainWindow>().As<Window>();
            builder.RegisterType<ViewModel>().AsSelf();

            builder.RegisterType<MyInterceptor>().AsSelf();
            builder.RegisterType<Model>().As<IModel>()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions
                {
                    Selector = new MyCustomInterceptorSelector()
                }).InterceptedBy(typeof(MyInterceptor));
        }
    }
}