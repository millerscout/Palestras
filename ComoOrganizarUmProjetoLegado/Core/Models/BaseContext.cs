using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
	public class BaseContext : DbContext
	{
		public BaseContext(string nameOrConnectionString)
			: base(CreateConnection(nameOrConnectionString), true)
		{
		}
		public BaseContext(string name, EntityConnection connection)
			: base(CreateConnection(name, connection), true)
		{
		}

		private static EntityConnection CreateConnection(string connectionString)
		{
			/*código para forçar deploy de dependências.*/
			Action<Type> noop = _ => { };
			noop(typeof(System.Data.Entity.SqlServer.SqlFunctions));
			/**/
			var builder = new EntityConnectionStringBuilder(ConfigurationManager.ConnectionStrings["CoreEntities"].ConnectionString);
			builder.Provider = "System.Data.SqlClient";



			//// como estou usando um MDF é necessário pegar o relative path.
			/// eu não queria utilizar sql express
			if (builder.ToString().Contains("##Path##"))
			{
				string codeBase = Assembly.GetExecutingAssembly().CodeBase;
				UriBuilder uri = new UriBuilder(codeBase);
				string path = Path.GetDirectoryName(Uri.UnescapeDataString(uri.Path));


				return new EntityConnection(builder.ToString().Replace("##Path##", path));
			}
			//////////////////////////////////////////////////////////////////////
			return new EntityConnection(builder.ToString());
		}
		private static EntityConnection CreateConnection(string connectionString, EntityConnection conn)
		{
			return conn;
		}


	}
}
