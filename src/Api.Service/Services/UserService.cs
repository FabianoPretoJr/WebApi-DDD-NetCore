using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.DTO.User;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Models;
using AutoMapper;

namespace Api.Service.Services
{
    public class UserService : IUserService
    {
        private IRepository<UserEntity> _reposiory;
        private readonly IMapper _mapper;

        public UserService(IRepository<UserEntity> repository, IMapper mapper)
        {
            _reposiory = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            var listEntity = await _reposiory.SelectAsync();
            return _mapper.Map<IEnumerable<UserDTO>>(listEntity);
        }

        public async Task<UserDTO> GetId(Guid id)
        {
            var entity =  await _reposiory.SelectAsync(id);
            return _mapper.Map<UserDTO>(entity) ?? new UserDTO();
        }

        public async Task<UserCreateResultDTO> Post(UserCreateDTO user)
        {
            var model = _mapper.Map<UserModel>(user);
            var entity = _mapper.Map<UserEntity>(model);
            var result =  await _reposiory.InsertAsync(entity);

            return _mapper.Map<UserCreateResultDTO>(result);
        }

        public async Task<UserUpdateResultDTO> Put(UserUpdateDTO user)
        {
            var model = _mapper.Map<UserModel>(user);
            var entity = _mapper.Map<UserEntity>(model);
            var result = await _reposiory.UpdateAsync(entity);

            return _mapper.Map<UserUpdateResultDTO>(result);
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _reposiory.DeleteAsync(id);
        }
    }
}