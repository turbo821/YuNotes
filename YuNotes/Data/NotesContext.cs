using Microsoft.EntityFrameworkCore;
using YuNotes.Models;

namespace YuNotes.Data
{
    public class NotesContext : DbContext
    {
        public DbSet<Note> Notes { get; set; } = null!;
        public DbSet<NoteGroup> Groups { get; set; } = null!;

        public NotesContext(DbContextOptions options)
            :base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NoteGroup>().HasData(

                new NoteGroup{ Id = Guid.NewGuid(), Name = "Work" },
                new NoteGroup { Id = Guid.NewGuid(), Name = "Life" },
                new NoteGroup { Id = Guid.NewGuid(), Name = "Personal" },
                new NoteGroup { Id = Guid.NewGuid(), Name = "Travel" }
                );

            modelBuilder.Entity<Note>()
                .HasKey(n => n.Id);

            modelBuilder.Entity<Note>()
                .HasOne(n => n.Group)
                .WithMany(g => g.Notes)
                .HasForeignKey(n => n.GroupId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<NoteGroup>()
                .HasKey(g => g.Id);

            modelBuilder.Entity<NoteGroup>()
                .HasMany(g => g.Notes)
                .WithOne(n => n.Group)
                .HasForeignKey(n => n.GroupId)
                .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
