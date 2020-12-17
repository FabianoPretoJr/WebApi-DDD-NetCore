using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services.User;

namespace Api.Service.Services
{
    public class UserService : IUserService
    {
        private IRepository<UserEntity> _reposiory;

        public UserService(IRepository<UserEntity> repository)
        {
            _reposiory = repository;
        }

        public async Task<IEnumerable<UserEntity>> GetAll()
        {
            return await _reposiory.SelectAsync();
        }

        public async Task<UserEntity> GetTask(Guid id)
        {
            return await _reposiory.SelectAsync(id);
        }

        public async Task<UserEntity> Post(UserEntity user)
        {
            return await _reposiory.InsertAsync(user);
        }

        public async Task<UserEntity> Put(UserEntity user)
        {
            return await _reposiory.UpdateAsync(user);
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _reposiory.DeleteAsync(id);
        }
    }
}