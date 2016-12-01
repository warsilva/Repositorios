using AutoMapper;
using ICICore.Mvc.Web;
using ICICore.Mvc.Web.Atributos;
using ICICore.Mvc.Web.Controllers;
using ICICore.Web.Configuracoes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ICICore.Web.Controllers
{
    /// <summary>
    /// Controlador de 'usuario'.
    /// </summary>
    public class UsuarioController : BaseController
    {
        #region Propriedades
        private Configuracao Config;
        private Negocio.Usuario negocio;
        private Negocio.Perfil negocioPerfil;
        #endregion

        /// <summary>
        /// Construtor.
        /// Atribui as dependências do controller.
        /// </summary>
        /// <param name="mapper">Dependência do AutoMapper.</param>
        /// <param name="config">Dependência do Config.</param>
        public UsuarioController(IMapper mapper, Configuracao config) : base(mapper, config)
        {
            this.Config = config;
            negocio = new Negocio.Usuario();
            negocioPerfil = new Negocio.Perfil();
        }

        [Route("/usuario")]
        [Route("/usuario/listar")]
        [Route("/usuario/pagina-{pagina}")]
        [Route("/usuario/listar/pagina-{pagina}")]
        public IActionResult Listar(int pagina = 1, ViewModels.Usuario modeloPesq = null)
        {
            ViewBag.PaginaAtual = pagina;
            ViewBag.Registros = (int)(Config.Aparencia.RegistrosPorPagina > 0 ? Config.Aparencia.RegistrosPorPagina
                : 10);
            ViewData["Listagem"] = PopularGrid(modeloPesq);
            return View(modeloPesq);
        }

        [Route("/usuario/inserir")]
        [HttpGet]
        public IActionResult Inserir()
        {
            //var usuario = new ViewModels.Usuario();
            //usuario.DataAdmissao = DateTime.Now;
            PopularPerfil(null);
            //return View(usuario);
            return View(new ViewModels.Usuario());
        }

        [Route("/usuario/inserir")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Inserir(ViewModels.Usuario modelo)
        {
            var titulo = typeof(ViewModels.Usuario).GetCustomAttribute<TituloAttribute>().Singular;
            if (ModelState.IsValid)
            {
                Entidades.Usuario entidade = Mapper.Map<ViewModels.Usuario, Entidades.Usuario>(modelo);
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

            PopularPerfil(modelo.PerfilId);
            return View(modelo);
        }

        [Route("/usuario/editar/{id?}")]
        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if (id == null)
                return NotFound();

            Entidades.Usuario modelo = negocio.Listar((int)id);
            if (modelo == null)
                return NotFound();

            ViewModels.Usuario usuario = Mapper.Map<Entidades.Usuario, ViewModels.Usuario>(modelo);
            PopularPerfil(usuario.PerfilId);
            return View(usuario);
        }

        [Route("/usuario/editar/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(int? id, ViewModels.Usuario modelo)
        {
            if (id == null)
                return NotFound();

            var titulo = typeof(ViewModels.Usuario).GetCustomAttribute<TituloAttribute>().Singular;
            if (ModelState.IsValid)
            {
                Entidades.Usuario entidade = Mapper.Map<ViewModels.Usuario, Entidades.Usuario>(modelo);
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

            PopularPerfil(modelo.PerfilId);
            return View(modelo);
        }

        [Route("/usuario/excluir/{id?}")]
        //[HttpPost]
        public IActionResult Excluir(int? id)
        {
            var titulo = typeof(ViewModels.Usuario).GetCustomAttribute<TituloAttribute>().Singular;
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

        private IEnumerable<ViewModels.Usuario> PopularGrid(ViewModels.Usuario modeloPesq)
        {
            var listEntidades = negocio.ListarPesquisa(modeloPesq.Nome);
            return Mapper.Map<IEnumerable<Entidades.Usuario>, IEnumerable<ViewModels.Usuario>>(listEntidades);
        }

        private void PopularPerfil(int? perfil)
        {
            List<ViewModels.Perfil> perfis = Mapper.Map<List<Entidades.Perfil>, List<ViewModels.Perfil>>
                (negocioPerfil.Listar().ToList());
            ViewBag.Perfil = new SelectList(perfis, "Id", "Nome");
        }
    }
}