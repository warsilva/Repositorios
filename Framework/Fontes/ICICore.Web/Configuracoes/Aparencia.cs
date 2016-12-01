using ICICore.Mvc.Web.Configuracoes;

namespace ICICore.Web.Configuracoes
{
	/// <summary>
	/// Configurações da interface.
	/// </summary>
	public class Aparencia
	{

		/// <summary>
		/// Quantidade de registros padrão por página.
		/// </summary>
		public uint RegistrosPorPagina
		{
			get {
				var leituraConfig = new LeituraAppSettings();
				var valor = leituraConfig.RetornarConfiguracao("RegistrosPorPagina").ToString();

				uint intTemp = 10;
				uint.TryParse(valor, out intTemp);
				return intTemp;

			} set { }
		}
	}
}
