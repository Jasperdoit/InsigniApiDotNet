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

            modelBuilder.Entity<ScoutAssignment>()
                .HasKey(sa => new { sa.ScoutId, sa.AssignmentId });

            modelBuilder.Entity<ScoutAssignment>()
                .HasOne(sa => sa.Scout)
                .WithMany(s => s.CompletedAssignments)
                .HasForeignKey(sa => sa.ScoutId);

            modelBuilder.Entity<ScoutAssignment>()
                .HasOne(sa => sa.Assignment)
                .WithMany(a => a.ScoutsWithAssignment)
                .HasForeignKey(sa => sa.AssignmentId);

            modelBuilder.Entity<ScoutInsignia>()
                .HasKey(si => new { si.ScoutId, si.InsigniaId });

            modelBuilder.Entity<ScoutInsignia>()
                .HasOne(si => si.Scout)
                .WithMany(s => s.CompletedInsignias)
                .HasForeignKey(si => si.ScoutId);

            modelBuilder.Entity<ScoutInsignia>()
                .HasOne(si => si.Insignia)
                .WithMany(i => i.ScoutsWithInsignia)
                .HasForeignKey(si => si.InsigniaId);
        }

        public DbSet<ScoutGroup> ScoutGroups { get; set; }
        public DbSet<Scout> Scouts { get; set; }
        public DbSet<Insignia> Insignias { get; set; }
        public DbSet<Assignment> Assignments { get; set; }

        public DbSet<ScoutAssignment> ScoutAssignments { get; set; }
        public DbSet<ScoutInsignia> ScoutInsignias { get; set; }
    }
}
