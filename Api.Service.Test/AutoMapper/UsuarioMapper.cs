using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
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

            var listaEntity = new List<UserEntity>();
            for (int i = 0; i < 5; i++)
            {
                listaEntity.Add(new UserEntity()
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Name.FullName(),
                    Email = Faker.Internet.Email(),
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow
                });
            }

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

            //ListEntity => ListDto
            var listaDto = Mapper.Map<List<UserDto>>(listaEntity);
            Assert.True(listaDto.Count() == listaEntity.Count);
            for (int i = 0; i < listaEntity.Count; i++)
            {
                Assert.Equal(listaDto[i].Id, listaEntity[i].Id);
                Assert.Equal(listaDto[i].Name, listaEntity[i].Name);
                Assert.Equal(listaDto[i].Email, listaEntity[i].Email);
                Assert.Equal(listaDto[i].CreateAt, listaEntity[i].CreateAt);
            }

            //UserEntity => UserDtoCreateResult
            var userCreateResult = Mapper.Map<UserDtoCreateResult>(dtoToEntity);
            Assert.Equal(dtoToEntity.Id, userCreateResult.Id);
            Assert.Equal(dtoToEntity.Name, userCreateResult.Name);
            Assert.Equal(dtoToEntity.Email, userCreateResult.Email);
            Assert.Equal(dtoToEntity.CreateAt, userCreateResult.CreateAt);

            //UserEntity => UserDtoUpdateResult
            var userUpdateResult = Mapper.Map<UserDtoUpdateResult>(dtoToEntity);
            Assert.Equal(dtoToEntity.Id, userUpdateResult.Id);
            Assert.Equal(dtoToEntity.Name, userUpdateResult.Name);
            Assert.Equal(dtoToEntity.Email, userUpdateResult.Email);
            Assert.Equal(dtoToEntity.UpdateAt, userUpdateResult.UpdateAt);

            //Dto => Model
            var userModel = Mapper.Map<UserModel>(entityToDto);
            Assert.Equal(userModel.Id, entityToDto.Id);
            Assert.Equal(userModel.Name, entityToDto.Name);
            Assert.Equal(userModel.Email, entityToDto.Email);
            Assert.Equal(userModel.CreateAt, entityToDto.CreateAt);

            //Model => DtoCreate
            var userDtoCreate = Mapper.Map<UserDtoCreate>(userModel);
            Assert.Equal(userModel.Name, userDtoCreate.Name);
            Assert.Equal(userModel.Email, userDtoCreate.Email);

            //Model => DtoUpdate
            var userDtoUpdate = Mapper.Map<UserDtoUpdate>(userModel);
            Assert.Equal(userModel.Id, userDtoUpdate.Id);
            Assert.Equal(userModel.Name, userDtoUpdate.Name);
            Assert.Equal(userModel.Email, userDtoUpdate.Email);
        }
    }
}
