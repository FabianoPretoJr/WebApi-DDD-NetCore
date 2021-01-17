using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.DTO.Uf;

namespace Api.Domain.Interfaces.Services.UF
{
    public interface IUfService
    {
        Task<UfDTO> GetId(Guid id);
        Task<IEnumerable<UfDTO>> GetAll();
    }
}