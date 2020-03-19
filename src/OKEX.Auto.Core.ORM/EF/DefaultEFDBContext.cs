using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace OKEX.Auto.Core.Context
{
    public class DefaultEFDBContext : DbContext
    {
        public DefaultEFDBContext(DbContextOptions<DefaultEFDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //查找所有FluentAPI配置
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes().Where(q => q.GetInterface(typeof(IEntityTypeConfiguration<>).FullName) != null);

            //应用FluentAPI
            foreach (var type in typesToRegister)
            {
                //dynamic使C#具有弱语言的特性，在编译时不对类型进行检查
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }
        }
    }

    public class DefaultEFDBContextDesignFactory : IDesignTimeDbContextFactory<DefaultEFDBContext>
    {
        public DefaultEFDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DefaultEFDBContext>()
                .UseNpgsql("Server=47.96.159.0;Database=OKEX_DB;User Id=postgres;Password=postgres123;");

            return new DefaultEFDBContext(optionsBuilder.Options);
        }
    }
}
