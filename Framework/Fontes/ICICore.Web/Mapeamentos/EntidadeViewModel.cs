using AutoMapper;

namespace ICICore.Web.Mapeamentos
{
	internal class EntidadeViewModel
	{
		public void Mapear(IMapperConfigurationExpression cfg)
		{
			cfg.CreateMap<ViewModels.Usuario, Entidades.Usuario>();
			cfg.CreateMap<Entidades.Usuario, ViewModels.Usuario>();

            cfg.CreateMap<ViewModels.Perfil, Entidades.Perfil>();
            cfg.CreateMap<Entidades.Perfil, ViewModels.Perfil>();
            
        }
	}
}