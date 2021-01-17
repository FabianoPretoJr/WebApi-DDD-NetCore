using System.Threading.Tasks;
using Api.Data.Data;
using Api.Data.Repository;
using Api.Domain.Entities;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Implementations
{
    public class CepImplementation : BaseRepository<CepEntity>, ICepRepository
    {
        private DbSet<CepEntity> _dataset;

        public CepImplementation(ApplicationDbContext database) : base(database)
        {
            _dataset = database.Set<CepEntity>();
        }

        public async Task<CepEntity> SelectAsync(string cep)
        {
            return await _dataset.Include(c => c.Municipio).ThenInclude(m => m.Uf).SingleOrDefaultAsync(u => u.Cep.Equals(cep));
        }
    }
}