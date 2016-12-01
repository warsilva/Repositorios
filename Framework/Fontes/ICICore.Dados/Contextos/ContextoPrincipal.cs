using ICICore.Dados.Configuracoes;
using ICICore.Dados.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ICICore.Dados.Contextos
{
	public class ContextoPrincipal : BaseContexto
	{
		#region DbSets
		public DbSet<Entidades.Usuario> Usuario { get; set; }

		public DbSet<Entidades.Perfil> Perfil { get; set; }
		#endregion

		#region Eventos
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
            
           
			var config = new Configuracao();
			optionsBuilder.UseSqlServer(config.Comum.ConexaoPrincipal);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.AdicionarConfiguracao(new FluentApi.Usuario());
			base.OnModelCreating(modelBuilder);
		}
		#endregion
	}
}