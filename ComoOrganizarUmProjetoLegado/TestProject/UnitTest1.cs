using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Repository;
using System.Linq;

namespace TestProject
{
	[TestClass]
	public class UnitTest1 : BaseTest
	{
		[TestMethod]
		public void DeveRetornarErroPoisPossuiNomeIlegalParaContato()
		{
			var repository = container.GetInstance<IContatosRepository>();


			Assert.IsTrue(repository.GetAll().Count() == 0);

			var exception = Assert.ThrowsException<CoreValidationException>(() =>
						{
							repository.Add(new Core.Models.Contato { Nome = "nome ilegal" });

							repository.Save();
						});




		}
	}
}
