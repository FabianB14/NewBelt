using Microsoft.EntityFrameworkCore;


namespace NewBelt.Models
{
    public class BeltContext : DbContext
    {


        public DbSet<Users> newusers {get; set;}
        public DbSet<Likers> likers {get; set;}
        public DbSet<Post> post {get;set;}
        

        // base() calls the parent class' constructor passing the "options" parameter along
        public BeltContext(DbContextOptions<BeltContext> options) : base(options) { }
    }
}