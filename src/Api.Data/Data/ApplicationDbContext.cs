using Api.Data.Mapping;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>(new UserMap().Configure);

            base.OnModelCreating(modelBuilder);
        }
    }
}