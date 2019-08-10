using Castle.Windsor;

namespace AnyCompany.Ioc
{
    public static class Bootstrapper
    {
        private static object _objLock = new object();

        private static WindsorContainer _container;

        public static WindsorContainer GetContainer()
        {
            if (_container != null)
                return _container;

            lock (_objLock)
            {
                if (_container != null)
                    return _container;

                var container = new WindsorContainer();
                container.Install(new DependencyInstaller());

                _container = container;
                return _container;
            }
        }
    }
}
