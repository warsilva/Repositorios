using System;

namespace ICICore.Entidades
{
	public class Usuario
	{
		public int Id { get; set; }
		public string Nome { get; set; }
		public DateTime DataAdmissao { get; set; }
		public int Status { get; set; }
		public int PerfilId { get; set; }
		public Perfil Perfil { get; set; } = new Perfil();
	}
}