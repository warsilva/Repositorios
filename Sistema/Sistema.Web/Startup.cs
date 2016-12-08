using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using Microsoft.AspNet.SignalR.Infrastructure;
using Sistema.Web;

namespace Sistema.Web
{
	public class Startup
	{
		public class SignalRContractResolver : IContractResolver
		{
			private readonly Assembly _assembly;
			private readonly IContractResolver _camelCaseContractResolver;
			private readonly IContractResolver _defaultContractSerializer;

			public SignalRContractResolver()
			{
				_defaultContractSerializer = new DefaultContractResolver();
				_camelCaseContractResolver = new CamelCasePropertyNamesContractResolver();
				_assembly = typeof(Connection).GetTypeInfo().Assembly;
			}


			public JsonContract ResolveContract(Type type)
			{
				if (type.GetTypeInfo().Assembly.Equals(_assembly))
					return _defaultContractSerializer.ResolveContract(type);

				return _camelCaseContractResolver.ResolveContract(type);
			}

		}



		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
					.SetBasePath(env.ContentRootPath)
					.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
					.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
					.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		public IConfigurationRoot Configuration { get; }

		//// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			var settings = new JsonSerializerSettings();
			settings.ContractResolver = new SignalRContractResolver();

			var serializer = JsonSerializer.Create(settings);
			services.Add(new ServiceDescriptor(typeof(JsonSerializer),
									 provider => serializer,
									 ServiceLifetime.Transient));

			// Add framework services.
			services.AddMvc();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseBrowserLink();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStaticFiles();
			app.UseSignalR2();
			app.UseMvc(routes =>
			{
				routes.MapRoute(
									name: "default",
									template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
