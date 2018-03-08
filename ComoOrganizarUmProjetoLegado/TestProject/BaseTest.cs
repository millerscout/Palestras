using Core.Models;
using Core.Repository;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
	public class BaseTest
	{
		private EntityConnection connection;
		private CoreEntities context;
		public Container container;
		public BaseTest()
		{
			ClearTest();


			container = new Container();
			container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
			InitializeContainer(container);
			container.Verify();

		}

		private void InitializeContainer(Container container)
		{
			container.Register<IContatosRepository>(() => new ContatosRepository(context));
		}
		public void ClearTest()
		{
			Effort.Provider.EffortProviderConfiguration.RegisterProvider();
			var Connection = ConfigurationManager.ConnectionStrings["CoreEntities"];
			connection = Effort.EntityConnectionFactory.CreateTransient(Connection.ConnectionString);
			context = new CoreEntities(connection);
			InitializeSeed();
		}

		private void InitializeSeed()
		{

		}
	}

}
