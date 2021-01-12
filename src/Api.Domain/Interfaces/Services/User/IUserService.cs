using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.DTO.User;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Services.User
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAll();
        Task<UserDTO> GetId(Guid id);
        Task<UserCreateResultDTO> Post(UserCreateDTO user);
        Task<UserUpdateResultDTO> Put(UserUpdateDTO user);
        Task<bool> Delete(Guid id);
    }
}