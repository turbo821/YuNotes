using Microsoft.EntityFrameworkCore;
using YuNotes.Data.Configurations;
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
            modelBuilder.ApplyConfiguration(new NoteConfiguration());
            modelBuilder.ApplyConfiguration(new NoteGroupConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());   
        }
    }
}
