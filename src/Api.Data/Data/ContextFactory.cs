using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Data.Data
{
    public class ContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var connectionString = "Server=localhost;Port=3306;DataBase=estudotdd;Uid=root;Pwd=root";
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseMySql(connectionString);
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}