using Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProjetoUtilizandoIoC.Controllers
{
	public class HomeController : ApiController
	{
		private readonly IContatosRepository rep;

		public HomeController(IContatosRepository conRep)
		{
			rep = conRep;
		}
		public IHttpActionResult Get()
		{
			return Ok(rep.GetAll());
		}
	}
}