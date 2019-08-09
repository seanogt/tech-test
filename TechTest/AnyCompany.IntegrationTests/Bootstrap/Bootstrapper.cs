using Castle.Windsor;

namespace AnyCompany.IntegrationTests.Bootstrap
{
    internal static class Bootstrapper
    {
        private static object _objLock = new object();

        private static WindsorContainer _container;

        internal static WindsorContainer GetContainer()
        {
            if (_container != null)
                return _container;

            lock (_objLock)
            {
                if (_container != null)
                    return _container;

                var container = new WindsorContainer();
                container.Install(new IntegrationTestInstaller());

                _container = container;
                return _container;
            }
        }
    }
}
