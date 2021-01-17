using Api.Data.Data;
using Api.Data.Repository;
using Api.Domain.Entities;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Implementations
{
    public class UfImplementation : BaseRepository<UfEntity>, IUfRepository
    {
        private DbSet<UfEntity> _dataset;

        public UfImplementation(ApplicationDbContext database) : base(database)
        {
            _dataset = database.Set<UfEntity>();
        }
    }
}