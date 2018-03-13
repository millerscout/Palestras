[assembly: WebActivator.PostApplicationStartMethod(typeof(ProjetoUtilizandoIoC.App_Start.ApiInitializer), "Initialize")]

namespace ProjetoUtilizandoIoC.App_Start
{
	using System.Web.Http;
	using SimpleInjector;
	using SimpleInjector.Integration.WebApi;
	using Core.Repository;
	using SimpleInjector.Lifestyles;

	public static class ApiInitializer
	{
		public static void Initialize()
		{
			var container = new Container();
			container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

			InitializeContainer(container);

			container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

			container.Verify();

			GlobalConfiguration.Configuration.DependencyResolver =
				new SimpleInjectorWebApiDependencyResolver(container);
		}

		private static void InitializeContainer(Container container)
		{
			container.Register<IContatosRepository>(() => new ContatosRepository());
		}
	}
}