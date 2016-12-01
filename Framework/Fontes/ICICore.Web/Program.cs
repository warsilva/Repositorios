using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace ICICore.Web
{
	public class Program
	{
		#region Métodos
		public static void Main(string[] args)
		{
			var host = new WebHostBuilder()
					.UseKestrel()
					.UseContentRoot(Directory.GetCurrentDirectory())
					.UseIISIntegration()
					.UseStartup<Startup>()
					.Build();

			host.Run();
		}
		#endregion
	}
}