using InsigniApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace InsigniApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<ScoutGroup>()
                .HasMany(sg => sg.Scouts)
                .WithOne(s => s.ScoutGroup)
                .HasForeignKey(s => s.ScoutGroupId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Scout>()
                .HasMany(s => s.CompletedAssignments)
                .WithMany(a => a.ScoutsWithAssignment)
                .UsingEntity(j => {
                    j.ToTable("ScoutAssignments");
                    j.Property<DateTime>("CompletedDate");
                    j.Property<String>("LeaderSignature");
                });

            modelBuilder.Entity<Scout>()
                .HasMany(s => s.CompletedInsignias)
                .WithMany(i => i.ScoutsWithInsignia)
                .UsingEntity(j => j.ToTable("ScoutInsignias"));

            modelBuilder.Entity<Insignia>()
                .HasMany(i => i.Assignments)
                .WithOne(a => a.Insignia)
                .HasForeignKey(a => a.InsigniaId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<ScoutGroup> ScoutGroups { get; set; }
        public DbSet<Scout> Scouts { get; set; }
        public DbSet<Insignia> Insignias { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
    }
}
