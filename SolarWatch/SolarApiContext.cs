using Microsoft.EntityFrameworkCore;
using SolarWatch.Model;

namespace SolarWatch;

public class SolarApiContext :DbContext
{
    public DbSet<City> Cities { get; set; }
    public DbSet<SolarData> SolarDatas { get; set; }
    
   
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=localhost,1433;Database=SolarApi;User Id=sa;Password=YourStrongPassword123!;Encrypt=false;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>()
            .Property(c => c.Id)
            .ValueGeneratedOnAdd();
        
        modelBuilder.Entity<SolarData>()
            .Property(sd => sd.Id)
            .ValueGeneratedOnAdd();

        base.OnModelCreating(modelBuilder);
    }
}