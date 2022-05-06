﻿using Api.Domain.Dtos;
using Api.Domain.Entities;
using System.Threading.Tasks;

namespace Api.Domain.Interfaces.Services.User
{
    public interface ILoginService
    {
        Task<object> FindByLogin(LoginDto email);
    }
}
