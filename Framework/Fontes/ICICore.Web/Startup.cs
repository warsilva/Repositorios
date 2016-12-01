using AutoMapper;
using ICICore.Mvc.Web;
using ICICore.Mvc.Web.Configuracoes;
using ICICore.Web.Configuracoes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NonFactors.Mvc.Grid;
using System;
using System.Linq;

namespace ICICore.Web
{
	public class Startup : BaseStartup
	{
		#region Métodos
		public Startup(IHostingEnvironment env) : base(env)
		{
			this.MapperConfiguration = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<BaseConfiguracao, Configuracao>();
				new Mapeamentos.EntidadeViewModel().Mapear(cfg);
			});
		}

		/// <summary>
		/// Este método é chamado em tempo de execução. Utilize para adicionar os serviços que a aplicação precisa.
		/// </summary>
		/// <param name="services"></param>
		public override void ConfigureServices(IServiceCollection services)
		{
			base.ConfigureServices(services);
			services.AddSingleton<IMapper>(MapperConfiguration.CreateMapper());
			services.AddScoped(s =>
			{
				//fazer mapeamento simples das propriedades e ler appsettings
				var baseConfig = s.GetService<BaseConfiguracao>();
				var config = new Configuracao();
				this.Mapper = new Mapper(this.MapperConfiguration);
				this.Mapper.Map<Configuracao>(baseConfig);
				this.Configuration.Bind(config);
				return config;
			});
			services.AddMvcGrid();
		}

		/// <summary>
		/// Este método é chamado em tempo de execução. Utilize para configurar requisições HTTP.
		/// </summary>
		/// <param name="app"></param>
		/// <param name="env"></param>
		/// <param name="loggerFactory"></param>
		public override void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			base.Configure(app, env, loggerFactory);
		}
		#endregion
	}
}
