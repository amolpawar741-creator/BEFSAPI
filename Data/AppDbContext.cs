using BEFS.Entities;
using Microsoft.EntityFrameworkCore;

namespace BEFS.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt):base(opt)
        {
            
        }

        public DbSet<Users> Users => Set<Users>();

        public DbSet<Menu> Menus { get; set; }


    }
}
