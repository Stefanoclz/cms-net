using cms_net.Models;
using Microsoft.EntityFrameworkCore;

namespace cms_net.Context
{
    public class CMSContext : DbContext
    {
        public DbSet<Page> Pages { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<ComponentDefinition> ComponentDefinitions { get; set; }
        public DbSet<Field> Fields { get; set; }

        public CMSContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=cms_net;Integrated Security=True");
        }

    }
}
