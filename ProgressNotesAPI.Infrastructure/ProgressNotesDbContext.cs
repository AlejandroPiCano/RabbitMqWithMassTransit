using Microsoft.EntityFrameworkCore;

namespace ProgressNotesAPI.Infrastructure
{
    public class ProgressNotesDbContext: DbContext
    {
       public ProgressNotesDbContext() : base() { }

        public ProgressNotesDbContext(DbContextOptions<ProgressNotesDbContext> options) : base(options) { }

        public DbSet<ProgressNote> ProgressNotes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=ProgressNotesDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProgressNote>().ToTable("Seguimientos")
                .HasKey(x => x.Id);
            modelBuilder.Entity<ProgressNote>()
                     .Property(c => c.Id)
                     .HasColumnName("Id");
            modelBuilder.Entity<ProgressNote>()
                        .Property(c => c.Description)
                        .HasColumnName("Descripcion");
            modelBuilder.Entity<ProgressNote>()
                       .Property(c => c.CreateDateTime)
                       .HasColumnName("FechaCreacion");
        }
    }
}