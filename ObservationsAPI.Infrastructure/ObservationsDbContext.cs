using Microsoft.EntityFrameworkCore;

namespace ObservationsAPI.Infrastructure
{
    public class ObservationsDbContext : DbContext
    {
        public ObservationsDbContext() : base() {
            //Database.EnsureCreated();
        }

        public ObservationsDbContext(DbContextOptions<ObservationsDbContext> options) : base(options)
        {
            //Database.Migrate();
            //Database.CX();
        }
        public DbSet<Observation> Observations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=ObservationsDB;Trusted_Connection=True;");
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region  Observation
            modelBuilder.Entity<Observation>().ToTable("Controles");
            modelBuilder.Entity<Observation>()
                      .Property(c => c.Id)
                      .HasColumnName("Id");
            modelBuilder.Entity<Observation>()
                        .Property(c => c.Description)
                        .HasColumnName("Descripcion").HasMaxLength(100);
            modelBuilder.Entity<Observation>()
                       .Property(c => c.CreateDateTime)
                       .HasColumnName("FechaCreacion");
            modelBuilder.Entity<Observation>()
                       .Property(c => c.Applyed)
                       .HasColumnName("Aplicado");

            #endregion

            modelBuilder.Entity<Observation>().HasKey(vf => new { vf.Id });
        }


    }
}