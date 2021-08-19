using Microsoft.EntityFrameworkCore;
using WebApi.Api.Entity;

namespace WebApi.Api.Db
{
    public class ShowDb : DbContext
    {
        // 因为我们使用AddDbContext到容器，所以此处必须得有带参数的构造函数
        public ShowDb(DbContextOptions options) : base(options)
        {

        }
        
        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<AuditInfo> AuditInfo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(@"server=.;database=ShowDb;uid=sa;pwd=123456;");
    }
}
