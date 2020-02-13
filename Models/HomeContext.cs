using Microsoft.EntityFrameworkCore;

namespace HobbyHub.Models
{
    public class HomeContext : DbContext
    {
        public HomeContext(DbContextOptions options) : base(options){}

        public DbSet<User> Users { get; set; }
        public DbSet<Hobby> Hobbies {get;set;}
        public DbSet<Rsvp> Rsvps {get;set;}
    }
}