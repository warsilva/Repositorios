using AutoMapper;
using ICICore.Mvc.Web.Controllers;
using ICICore.Web.Configuracoes;
using Microsoft.AspNetCore.Mvc;

namespace ICICore.Web.Controllers
{
	/// <summary>
	/// Controler inicial do projeto.
	/// Não renomear.
	/// </summary>
	public class InicioController : BaseController
	{
		#region Construtor
		/// <summary>
		/// Construtor.
		/// Atribui as dependências do controller.
		/// </summary>
		/// <param name="mapper">Dependência do AutoMapper.</param>
		/// <param name="config">Dependência do Config.</param>
		public InicioController(IMapper mapper, Configuracao config) : base(mapper, config)
		{
		}
		#endregion

		#region Actions
		/// <summary>
		/// Ação de entrada do projeto.
		/// Não renomear.
		/// </summary>
		/// <returns></returns>
		[Route("/")]
		[Route("/inicio")]
		public IActionResult Inicial()
		{
			ViewData["Titulo"] = "Página Inicial";
			return View();
		}

		public IActionResult Login()
		{
			return View();
		}

		public IActionResult Acesso()
		{
			return View();
		}
		#endregion
	}
}
