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
        }
    }
}
