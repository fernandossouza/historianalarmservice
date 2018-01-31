using Microsoft.EntityFrameworkCore;
using historianalarmservice.Model;

namespace historianalarmservice.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Alarm> AlarmCurrents { get; set; }
        public DbSet<HistorianAlarm> HistorianAlarms { get; set; }
        
    }
}