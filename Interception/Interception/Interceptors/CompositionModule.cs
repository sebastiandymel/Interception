using System.Windows;
using Unity;
using Unity.Extension;
using Unity.Interception.ContainerIntegration;
using Unity.Interception.Interceptors.InstanceInterceptors.InterfaceInterception;
using Unity.Lifetime;

namespace SEDYInterception
{
    public class CompositionModule : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<Window, MainWindow>("Main", new ContainerControlledLifetimeManager());
            Container.RegisterType<ViewModel, ViewModel>(new ContainerControlledLifetimeManager());

            Container.RegisterType<IModel, Model>(
                new ContainerControlledLifetimeManager(),
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<MyInterceptionBehavior>(),
                new InterceptionBehavior<InterceptTaskBehavior>());



            #region Queuing

            //Container.RegisterType<IHeavyCalculationModel, HeavyCalculator>(
            //    new ContainerControlledLifetimeManager());


            Container.RegisterType<IHeavyCalculationModel, HeavyCalculator>(
                new ContainerControlledLifetimeManager(),
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<QueueInterception>());

            #endregion Queuing
        }
    }
}