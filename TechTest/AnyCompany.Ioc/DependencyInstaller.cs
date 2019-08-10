using System.Configuration;
using AnyCompany.Data.Contract.Repositories;
using AnyCompany.Data.Dapper.Factories;
using AnyCompany.Data.Dapper.Repositories;
using AnyCompany.Services.Services;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace AnyCompany.Ioc
{
    public class DependencyInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IOrderService, OrderService>());

            container.Register(
                Component.For<ICustomerOrderService, CustomerOrderService>());

            container.Register(
                Component.For<IOrderRepository, OrderRepository>());

            container.Register(
                Component.For<ICustomerRepository, CustomerRepositoryWrapper>());

            container.Register(
                Component.For<IConnectionFactory>().UsingFactoryMethod(CreateConnectionFactory));
        }

        private IConnectionFactory CreateConnectionFactory(IKernel kernel)
        {
            return new ConnectionFactory(
                ConfigurationManager.ConnectionStrings["CustomerConnectionString"].ConnectionString,
                ConfigurationManager.ConnectionStrings["OrderConnectionString"].ConnectionString);
        }
    }
}
