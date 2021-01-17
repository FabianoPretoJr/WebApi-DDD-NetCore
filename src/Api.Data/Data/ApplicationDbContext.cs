using System;
using Api.Data.Mapping;
using Api.Data.Seeds;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UfEntity> Ufs { get; set; }
        public DbSet<MunicipioEntity> Municipios { get; set; }
        public DbSet<CepEntity> Ceps { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserEntity>(new UserMap().Configure);
            modelBuilder.Entity<UfEntity>(new UfMap().Configure);
            modelBuilder.Entity<MunicipioEntity>(new MunicipioMap().Configure);
            modelBuilder.Entity<CepEntity>(new CepMap().Configure);

            modelBuilder.Entity<UserEntity>().HasData(
                new UserEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Administrador",
                    Email = "admin@mail.com",
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now
                }
            );

            UfSeeds.Ufs(modelBuilder);
        }
    }
}