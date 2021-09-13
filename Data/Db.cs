using GraphqlDemo.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace GraphqlDemo.Data
{
    public class Db : DbContext
    {
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Command> Commands { get; set; }
        
        public Db(DbContextOptions options) : base(options)
        {
            
        }
    }
}