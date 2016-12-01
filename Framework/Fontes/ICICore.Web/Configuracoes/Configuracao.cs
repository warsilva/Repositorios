using ICICore.Mvc.Web.Configuracoes;
using System;
using System.Linq;

namespace ICICore.Web.Configuracoes
{
	/// <summary>
	/// Classe agrupadora de configurações.
	/// </summary>
	public class Configuracao : BaseConfiguracao
	{
		private Comum comum;
		private Aparencia aparencia;

		public Comum Comum
		{
			get
			{
				if (comum == null)
				{
					comum = new Comum();
				}
				return comum;
			}
		}

		public Aparencia Aparencia
		{
			get
			{
				if (aparencia == null)
				{
					aparencia = new Aparencia();
				}
				return aparencia;
			}
		}
	}
}
