using ICICore.Dados.EntityFramework;
using System.Linq;

namespace ICICore.Negocio
{
	public class Usuario
	{
		#region Métodos
		public void Alterar(Entidades.Usuario usuario)
		{
			Dados.Repositorios.Usuario repositorio = new Dados.Repositorios.Usuario();
			repositorio.Alterar(usuario);
		}
		
		public void Excluir(Entidades.Usuario usuario)
		{
			Dados.Repositorios.Usuario repositorio = new Dados.Repositorios.Usuario();
			repositorio.Excluir(usuario);
		}

		public void Incluir(Entidades.Usuario usuario)
		{
			Dados.Repositorios.Usuario repositorio = new Dados.Repositorios.Usuario();
			repositorio.Incluir(usuario);
		}

		public int IncluirRetornandoId(Entidades.Usuario usuario)
		{
			Dados.Repositorios.Usuario repositorio = new Dados.Repositorios.Usuario();
			return repositorio.IncluirRetornandoId(usuario);
		}

		public IQueryable<Entidades.Usuario> Listar()
		{
			Dados.Repositorios.Usuario repositorio = new Dados.Repositorios.Usuario();
			return repositorio.Listar();
		}

		public Entidades.Usuario Listar(int id)
		{
			Dados.Repositorios.Usuario repositorio = new Dados.Repositorios.Usuario();
			return repositorio.Listar(id);
		}
		
		public void Excluir(int? id)
		{
			Dados.Repositorios.Usuario repositorio = new Dados.Repositorios.Usuario();
			repositorio.Excluir(id.Value);
		}
		
		public IQueryable<Entidades.Usuario> ListarPesquisa(string nome)
		{
			Dados.Repositorios.Usuario repositorio = new Dados.Repositorios.Usuario();
			nome = nome?.Trim().TrimEnd();
			return repositorio.Listar(x => {
				return (nome == null || x.Nome.ToLower().Contains(nome?.ToLower()));
			});
		}

		public IQueryable<Entidades.Usuario> Listar(int pagina, int totalRegistrosPagina, string campoOrdenacao, Ordenacao ordenacao)
		{
			Dados.Repositorios.Usuario repositorio = new Dados.Repositorios.Usuario();
			return repositorio.Listar(pagina, totalRegistrosPagina, campoOrdenacao, ordenacao);
		}
		#endregion
	}
}