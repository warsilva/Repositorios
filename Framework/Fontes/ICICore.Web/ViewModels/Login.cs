using System.ComponentModel.DataAnnotations;

namespace ICICore.Web.ViewModels
{
	public class Login
	{
		[Display(Prompt = "Usuário")]
		public string Usuario { get; set; }

		[Display(Prompt = "E-mail")]
		public string Email { get; set; }

		[Display(Prompt = "Senha")]
		[DataType(DataType.Password)]
		public string Senha { get; set; }

		[Display(Name = "Lembrar senha")]
		public bool LembrarSenha { get; set; }
	}
}