using Api.CrossCutting.Mappings;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public static class ConfigureMapping
    {
        public static void ConfigureMappingInjection(this IServiceCollection service)
        {
            MapperConfiguration config = MapperConfigure();

            IMapper mapper = config.CreateMapper();

            service.AddSingleton(mapper);
        }

        public static MapperConfiguration MapperConfigure()
        {
            return new MapperConfiguration(x =>
            {
                x.AddProfile(new DtoToModelProfile());
                x.AddProfile(new EntityToDtoProfile());
                x.AddProfile(new ModelToEntityProfile());
            });
        }
    }
}
