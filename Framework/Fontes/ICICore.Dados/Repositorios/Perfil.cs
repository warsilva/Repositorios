using ICICore.Dados.EntityFramework;

namespace ICICore.Dados.Repositorios
{
	public class Perfil : BaseRepositorio<Entidades.Perfil, int>
	{
		public Perfil()
		{
			this.Contexto = new Contextos.ContextoPrincipal();
		}

		public int IncluirRetornandoId(Entidades.Perfil entidade)
		{
			if (entidade != null)
			{
				this.Contexto.Add<Entidades.Perfil>(entidade);
				this.Contexto.SaveChanges();
			}
			return entidade.Id;
		}
	}
}