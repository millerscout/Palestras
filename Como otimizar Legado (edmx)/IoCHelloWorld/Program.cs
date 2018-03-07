using Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoCHelloWorld
{
	class Program
	{
		static void Main(string[] args)
		{

			var rep = new ContatosRepository();

			Console.WriteLine(rep.GetAll().Count());
		}
	}
}
