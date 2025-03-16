using Microsoft.EntityFrameworkCore;

namespace apief
{
    public class DataContextEF : DbContext
    {
        private readonly IConfiguration _config;

        public DataContextEF(IConfiguration config)
        {
            _config = config;
        }

        public virtual DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(_config.GetConnectionString("DefaultConnection"),
                        optionsBuilder => optionsBuilder.EnableRetryOnFailure());
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Dbo");

            modelBuilder.Entity<User>()
            .ToTable("User", "Dbo")
               .HasKey(u => u.id);

            modelBuilder.HasDefaultSchema("Dbo");

            modelBuilder.Entity<Note>()
                .ToTable("Notes", "Dbo")
                .HasKey(n => n.noteId); 

            modelBuilder.Entity<Note>()
                .Property(n => n.id)
                .IsRequired();

            modelBuilder.Entity<Note>()
                .Property(n => n.title)
                .IsRequired(false); 

            modelBuilder.Entity<Note>()
                .Property(n => n.description)
                .IsRequired(false);

            modelBuilder.Entity<Note>()
                .Property(n => n.done)
                .IsRequired(false);
        }
    }
}

