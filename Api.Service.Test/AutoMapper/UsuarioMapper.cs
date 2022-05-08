using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Domain.Models.User;
using System;
using Xunit;

namespace Api.Service.Test.AutoMapper
{
    public class UsuarioMapper : BaseTestService
    {
        [Fact(DisplayName = "É possível Mapper dos modelos.")]
        public void E_possivel_mapear_modelos()
        {
            var model = new UserModel()
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow
            };

            //Model => Entity
            var dtoToEntity = Mapper.Map<UserEntity>(model);
            Assert.Equal(dtoToEntity.Id, model.Id);
            Assert.Equal(dtoToEntity.Name, model.Name);
            Assert.Equal(dtoToEntity.Email, model.Email);
            Assert.Equal(dtoToEntity.CreateAt, model.CreateAt);
            Assert.Equal(dtoToEntity.UpdateAt, model.UpdateAt);

            //Entity => Dto
            var entityToDto = Mapper.Map<UserDto>(dtoToEntity);
            Assert.Equal(dtoToEntity.Id, entityToDto.Id);
            Assert.Equal(dtoToEntity.Name, entityToDto.Name);
            Assert.Equal(dtoToEntity.Email, entityToDto.Email);
            Assert.Equal(dtoToEntity.CreateAt, entityToDto.CreateAt);
        }
    }
}
