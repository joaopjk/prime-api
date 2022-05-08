using Api.CrossCutting.Mappings;
using AutoMapper;
using System;

namespace Api.Service.Test
{
    public abstract class BaseTestService
    {
        public IMapper Mapper { get; set; }
        public BaseTestService()
        {
            Mapper = new AutoMapperFixture().GetMapper();
        }

        public class AutoMapperFixture : IDisposable
        {
            public IMapper GetMapper()
            {
                return new MapperConfiguration(x =>
                {
                    x.AddProfile(new DtoToModelProfile());
                    x.AddProfile(new EntityToDtoProfile());
                    x.AddProfile(new ModelToEntityProfile());
                }).CreateMapper();
            }

            public void Dispose()
            {

            }
        }
    }
}
