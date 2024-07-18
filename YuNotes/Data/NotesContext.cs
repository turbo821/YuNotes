using Microsoft.EntityFrameworkCore;
using YuNotes.Models;

namespace YuNotes.Data
{
    public class NotesContext : DbContext
    {
        public DbSet<NoteModel> Notes { get; set; } = null!;
        //public DbSet<NoteGroupModel> Groups = null!;

        public NotesContext(DbContextOptions options)
            :base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<NoteGroupModel>().HasData(
            //    new NoteGroupModel("Work"),
            //    new NoteGroupModel("Life"),
            //    new NoteGroupModel("Personal"),
            //    new NoteGroupModel("Travel")
            //    );
            modelBuilder.Entity<NoteModel>().HasData(
                new NoteModel() { Id = Guid.NewGuid(), Title = "Первая заметка", Text = "Текстовый текст" }
                );
        }
    }
}
