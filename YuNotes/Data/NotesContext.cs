using Microsoft.EntityFrameworkCore;
using YuNotes.Models;

namespace YuNotes.Data
{
    public class NotesContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Note> Notes { get; set; } = null!;
        public DbSet<NoteGroup> Groups { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

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

            modelBuilder.Entity<Note>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notes)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<NoteGroup>()
                .HasKey(g => g.Id);

            modelBuilder.Entity<NoteGroup>()
                .HasMany(g => g.Notes)
                .WithOne(n => n.Group)
                .HasForeignKey(n => n.GroupId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Notes)
                .WithOne(n => n.User)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Nickname)
                .IsUnique();
        }
    }
}
