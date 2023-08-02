using Microsoft.EntityFrameworkCore;

namespace GenerateTicket.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<TicketInfo> TicketInfo { get; set; }
    }
}
