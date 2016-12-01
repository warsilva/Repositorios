using ICICore.Dados.EntityFramework;
using System.Linq;

namespace ICICore.Negocio
{
	public class Perfil
	{
		#region Métodos
		public void Alterar(Entidades.Perfil perfil)
		{
			Dados.Repositorios.Perfil repositorio = new Dados.Repositorios.Perfil();
			repositorio.Alterar(perfil);
		}

		public void Excluir(Entidades.Perfil perfil)
		{
			Dados.Repositorios.Perfil repositorio = new Dados.Repositorios.Perfil();
			repositorio.Excluir(perfil);
		}

		public void Incluir(Entidades.Perfil perfil)
		{
			Dados.Repositorios.Perfil repositorio = new Dados.Repositorios.Perfil();
			repositorio.Incluir(perfil);
		}

		public int IncluirRetornandoId(Entidades.Perfil perfil)
		{
			Dados.Repositorios.Perfil repositorio = new Dados.Repositorios.Perfil();
			return repositorio.IncluirRetornandoId(perfil);
		}

		public IQueryable<Entidades.Perfil> Listar()
		{
			Dados.Repositorios.Perfil repositorio = new Dados.Repositorios.Perfil();
			return repositorio.Listar();
		}

		public Entidades.Perfil Listar(int id)
		{
			Dados.Repositorios.Perfil repositorio = new Dados.Repositorios.Perfil();
			return repositorio.Listar(id);
		}

		public void Excluir(int? id)
		{
			Dados.Repositorios.Perfil repositorio = new Dados.Repositorios.Perfil();
			repositorio.Excluir(id.Value);
		}

		public IQueryable<Entidades.Perfil> ListarPesquisa(string nome)
		{
			Dados.Repositorios.Perfil repositorio = new Dados.Repositorios.Perfil();
			nome = nome?.Trim();
			return repositorio.Listar(x => {
				return (nome == null || x.Nome.ToLower().Contains(nome?.ToLower()));
			});
		}

		public IQueryable<Entidades.Perfil> Listar(int pagina, int totalRegistrosPagina, string campoOrdenacao, Ordenacao ordenacao)
		{
			Dados.Repositorios.Perfil repositorio = new Dados.Repositorios.Perfil();
			return repositorio.Listar(pagina, totalRegistrosPagina, campoOrdenacao, ordenacao);
		}
		#endregion
	}
}