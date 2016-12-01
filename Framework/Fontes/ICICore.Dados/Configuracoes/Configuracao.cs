using ICICore.Dados.EntityFramework.Configuracoes;

namespace ICICore.Dados.Configuracoes
{
	/// <summary>
	/// Classe agrupadora de configurações
	/// </summary>
	public class Configuracao : BaseConfiguracao
	{
		#region Atributos
		private Comum comum;
		#endregion

		#region Construtores
		public Comum Comum
		{
			get
			{
				if (comum == null)
					comum = new Comum();
				return comum;
			}
		}
		#endregion
	}
}