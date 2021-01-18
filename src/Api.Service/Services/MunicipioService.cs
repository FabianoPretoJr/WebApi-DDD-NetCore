using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.DTO.Municipio;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.Municipio;
using Api.Domain.Models;
using Api.Domain.Repository;
using AutoMapper;

namespace Api.Service.Services
{
    public class MunicipioService : IMunicipioService
    {
        private IMunicipioRepository _repository;
        private readonly IMapper _mapper;

        public MunicipioService(IMunicipioRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<MunicipioDTO> GetId(Guid id)
        {
            var entity = await _repository.SelectAsync(id);
            return _mapper.Map<MunicipioDTO>(entity);
        }

        public async Task<IEnumerable<MunicipioDTO>> GetAll()
        {
            var listaEntity = await _repository.SelectAsync();
            return _mapper.Map<IEnumerable<MunicipioDTO>>(listaEntity);
        }

        public async Task<MunicipioCompletoDTO> GetCompleteById(Guid id)
        {
            var entity = await _repository.GetCompleteById(id);
            return _mapper.Map<MunicipioCompletoDTO>(entity);
        }

        public async Task<MunicipioCompletoDTO> GetCompleteByIBGE(int codIBGE)
        {
            var entity = await _repository.GetCompleteByIBGE(codIBGE);
            return _mapper.Map<MunicipioCompletoDTO>(entity);
        }

        public async Task<MunicipioCreateResultDTO> Post(MunicipioCreateDTO municipio)
        {
            var model = _mapper.Map<MunicipioModel>(municipio);
            var entity = _mapper.Map<MunicipioEntity>(model);
            var result = await _repository.InsertAsync(entity);
            return _mapper.Map<MunicipioCreateResultDTO>(result);
        }

        public async Task<MunicipioUpdateResultDTO> Put(MunicipioUpdateDTO municipio)
        {
            var model = _mapper.Map<MunicipioModel>(municipio);
            var entity = _mapper.Map<MunicipioEntity>(model);
            var result = await _repository.UpdateAsync(entity);
            return _mapper.Map<MunicipioUpdateResultDTO>(result);
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}