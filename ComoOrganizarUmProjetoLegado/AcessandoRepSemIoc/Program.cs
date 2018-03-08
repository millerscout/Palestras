using Core.Models;
using Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExemploSemIoC
{
	class Program
	{
		static void Main(string[] args)
		{

			var rep = new ContatosRepository();

			try
			{
				rep.Add(new Contato { Nome = "ilegal" });
				rep.Save();
			}
			catch (CoreValidationException ex)
			{
				ex.Errors.ToList().ForEach(q => { Console.WriteLine($"Prop:{q.Key}, Lista de Errors: {String.Join(",", q.Value)}"); });
			}
		}
	}
}
