using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using FavouriteLibrary.Dependency;
using Xamarin.Forms;

namespace FavouriteLibrary
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            ConfigureAutofac(new DependencyModule());
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
