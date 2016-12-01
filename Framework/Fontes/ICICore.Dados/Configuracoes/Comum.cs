using ICICore.Dados.EntityFramework.Configuracoes;
using System;

namespace ICICore.Dados.Configuracoes
{
	/// <summary>
	/// Configurações sem categorização.
	/// </summary>
	public class Comum
	{
		#region Propriedades
		/// <summary>
		/// Tempo de validade de uma sessão ociosa.
		/// Padrão: 15 minutos.
		/// </summary>
		public string ConexaoPrincipal
		{
			get
			{
				var leituraConfig = new LeituraAppSettings();
				var valor = leituraConfig.RetornarConfiguracao(nameof(ConexaoPrincipal)).ToString();
				return valor;
			}
		}
        
        #endregion
    }
}