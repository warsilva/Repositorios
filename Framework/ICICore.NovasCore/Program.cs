using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICICore.NovasCore
{
	class Program
	{
		static void Main(string[] args)
		{
			var pessoa = new Pessoa();

			//pessoa.Nome = "Jose mmsd";

			var p = pessoa.Nome?.ToLower();

			Console.WriteLine(p);
			Console.ReadKey();
		}


		public class Pessoa
		{
			public int Id { get; set; }
			public string Nome { get; set; }
		}
	}
}
