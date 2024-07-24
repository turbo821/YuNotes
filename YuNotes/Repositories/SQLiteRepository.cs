using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YuNotes.Data;
using YuNotes.Models;

namespace YuNotes.Repositories
{
    public class SQLiteRepository
    {
        NotesContext db;
        IEnumerable<Note>? notes;
        IEnumerable<NoteGroup>? groups;

        public SQLiteRepository(NotesContext context)
        {
            db = context;
        }

        public async Task GetAllNotes()
        {
            notes = await db.Notes.ToListAsync();
            groups = await db.Groups.ToListAsync();
        }

        public async Task<Note> GetNote(Guid id)
        {
            Note note;
            if (db.Notes.Any(n => n.Id == id))
            {
                note = await db.Notes.FindAsync(id);
                
                if(note == null)
                {
                    note = new() { Id = id };
                }
            }
            else
                note = new() { Id = id };

            return note;
        }
    }
}
