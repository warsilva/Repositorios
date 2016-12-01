using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace ICICore.Web.Views
{
	/// <summary>
	/// Funções compartilhadas pelas views geradas.
	/// </summary>
	public static class ViewExecutor
	{
		/// <summary>
		/// Transforma um enumerador em uma lista de opções para combo.
		/// </summary>
		/// <param name="enumerador">Campo Enumerador.</param>
		/// <returns></returns>
		public static IEnumerable<SelectListItem> ObterLista(Enum enumerador)
		{
			foreach (var item in enumerador.GetType().GetEnumValues())
			{
				yield return new SelectListItem()
				{
					Value = item.ToString(),
					Text = item.ToString(),
				};
			}
		}
	}
}