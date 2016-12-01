using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ICICore.Mvc.Web.Controllers;
using ICICore.Mvc.Web.Configuracoes;

namespace ICICore.Web.Controllers
{
	public class ConfiguracaoController : BaseController
	{
		public ConfiguracaoController(IMapper mapper, BaseConfiguracao config) : base(mapper, config)
		{
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Passo2()
		{
			return View();
		}

		public IActionResult Passo3()
		{
			return View();
		}

		public IActionResult Passo4()
		{
			return View();
		}
	}
}