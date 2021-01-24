using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using FavouriteLibrary.Dependency;
using MonkeyCache.FileStore;

namespace FavouriteLibrary
{
    public partial class App
    {
        public App()
        {
            InitializeComponent();

            ConfigureAutofac(new DependencyModule());
            Barrel.ApplicationId = "FavouriteLibrary";
            MainPage = new AppShell();
        }

        private void ConfigureAutofac(Module module)
        {
            var builder = new ContainerBuilder();

            if (module != null)
            {
                builder.RegisterModule(module);
            }

            var container = builder.Build();
            var csl = new AutofacServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => csl);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
