using ICICore.Dados.EntityFramework.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ICICore.Dados.FluentApi
{
	public class Usuario : ConfiguraEntidade<Entidades.Usuario>
	{
		public override void Configure(EntityTypeBuilder<Entidades.Usuario> entidade)
		{
			entidade.Property(e => e.Nome).HasColumnType("varchar(200)");
		}
	}
}