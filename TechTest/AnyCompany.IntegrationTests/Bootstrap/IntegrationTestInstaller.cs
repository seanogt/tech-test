using AnyCompany.Data.Contract.Repositories;
using AnyCompany.Data.Dapper.Repositories;
using AnyCompany.Services;
using AnyCompany.Services.Services;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace AnyCompany.IntegrationTests.Bootstrap
{
    public class IntegrationTestInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IOrderService, OrderService>());

            container.Register(
                Component.For<IOrderRepository, OrderRepository>());

            container.Register(
                Component.For<ICustomerRepository, CustomerRepositoryWrapper>());
        }
    }
}
