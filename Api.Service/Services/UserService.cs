using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Models.User;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<UserEntity> _repository;
        private readonly IMapper _mapper;

        public UserService(IRepository<UserEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<UserDto> Get(Guid id)
        {
            return _mapper.Map<UserDto>(await _repository.SelectAsync(id)) ?? new UserDto();
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            return _mapper.Map<IEnumerable<UserDto>>(await _repository.SelectAsync());
        }

        public async Task<UserDtoCreateResult> Post(UserDtoCreate user)
        {
            var userEntity = _mapper.Map<UserEntity>(_mapper.Map<UserModel>(user));
            return _mapper.Map<UserDtoCreateResult>(await _repository.InsertAsync(userEntity));
        }

        public async Task<UserDtoUpdateResult> Put(UserDtoUpdate user)
        {
            var userEntity = _mapper.Map<UserEntity>(_mapper.Map<UserModel>(user));
            return _mapper.Map<UserDtoUpdateResult>(await _repository.UpdateAsync(userEntity));
        }
    }
}
