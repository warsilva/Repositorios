////<!--
using AutoMapper;
using ICICore.Mvc.Web;
using ICICore.Mvc.Web.Atributos;
using ICICore.Mvc.Web.Controllers;
using ICICore.Web.Configuracoes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace ICICore.Web.Controllers
{
    /// <summary>
    /// Controlador de 'perfil'.
    /// </summary>
    public class PerfilController : BaseController
    {
        #region Propriedades
        private Configuracao Config;
        private Negocio.Perfil negocio;
        #endregion

        /// <summary>
        /// Construtor.
        /// Atribui as dependências do controller.
        /// </summary>
        /// <param name="mapper">Dependência do AutoMapper.</param>
        /// <param name="config">Dependência do Config.</param>
        public PerfilController(IMapper mapper, Configuracao config) : base(mapper, config)
        {
            this.Config = config;
            negocio = new Negocio.Perfil();
        }

        [Route("/perfil")]
        [Route("/perfil/listar")]
        [Route("/perfil/pagina-{pagina}")]
        [Route("/perfil/listar/pagina-{pagina}")]
        public IActionResult Listar(int pagina = 1, ViewModels.Perfil modeloPesq = null)
        {
            ViewBag.PaginaAtual = pagina;
            ViewBag.Registros = (int)(Config.Aparencia.RegistrosPorPagina > 0 ? Config.Aparencia.RegistrosPorPagina : 10);
            ViewData["Listagem"] = PopularGrid(modeloPesq);
            return View(modeloPesq);
        }

        [Route("/perfil/inserir")]
        [HttpGet]
        public IActionResult Inserir()
        {
            return View(new ViewModels.Perfil());
        }

        [Route("/perfil/inserir")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Inserir(ViewModels.Perfil modelo)
        {
            var titulo = typeof(ViewModels.Perfil).GetCustomAttribute<TituloAttribute>().Singular;
            if (ModelState.IsValid)
            {
                Entidades.Perfil entidade = Mapper.Map<ViewModels.Perfil, Entidades.Perfil>(modelo);
                int idEntidade = negocio.IncluirRetornandoId(entidade);

                TempData.Add(nameof(ResultadoOperacao), new ResultadoOperacao()
                {
                    Resultado = TipoAlerta.Sucesso,
                    Mensagem = Mensagens.SucessoSalvar(titulo),
                });

                return RedirectToAction("Editar", new { id = idEntidade });
            }
            else
            {
                TempData.Add(nameof(ResultadoOperacao), new ResultadoOperacao()
                {
                    Resultado = TipoAlerta.Alerta,
                    Mensagem = Mensagens.ErroCampos(),
                });
            }
            return View(modelo);
        }

        [Route("/perfil/editar/{id?}")]
        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if (id == null)
                return NotFound();

            Entidades.Perfil modelo = negocio.Listar((int)id);
            if (modelo == null)
                return NotFound();
            return View(Mapper.Map<Entidades.Perfil, ViewModels.Perfil>(modelo));
        }

        [Route("/perfil/editar/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(int? id, ViewModels.Perfil modelo)
        {
            if (id == null)
                return NotFound();

            var titulo = typeof(ViewModels.Perfil).GetCustomAttribute<TituloAttribute>().Singular;
            if (ModelState.IsValid)
            {
                Entidades.Perfil entidade = Mapper.Map<ViewModels.Perfil, Entidades.Perfil>(modelo);
                negocio.Alterar(entidade);

                TempData.Add(nameof(ResultadoOperacao), new ResultadoOperacao()
                {
                    Resultado = TipoAlerta.Sucesso,
                    Mensagem = Mensagens.SucessoSalvar(titulo),
                });
            }
            else
            {
                TempData.Add(nameof(ResultadoOperacao), new ResultadoOperacao()
                {
                    Resultado = TipoAlerta.Alerta,
                    Mensagem = Mensagens.ErroCampos(),
                });
            }

            return View(modelo);
        }

        [Route("/perfil/excluir/{id?}")]
        [HttpPost]
        public IActionResult Excluir(int? id)
        {
            var titulo = typeof(ViewModels.Perfil).GetCustomAttribute<TituloAttribute>().Singular;
            if (id != null && id > 0)
            {
                negocio.Excluir(id);

                TempData.Add(nameof(ResultadoOperacao), new ResultadoOperacao()
                {
                    Resultado = TipoAlerta.Sucesso,
                    Mensagem = Mensagens.SucessoExcluir(titulo, id),
                });

                return RedirectToAction("Listar");
            }
            return View();
        }

        private IEnumerable<ViewModels.Perfil> PopularGrid(ViewModels.Perfil modeloPesq)
        {
            var listEntidades = negocio.ListarPesquisa(modeloPesq.Nome);
            return Mapper.Map<IEnumerable<Entidades.Perfil>, IEnumerable<ViewModels.Perfil>>(listEntidades);
        }
    }
}