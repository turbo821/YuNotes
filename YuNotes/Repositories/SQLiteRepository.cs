using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YuNotes.Data;
using YuNotes.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace YuNotes.Repositories
{
    public class SQLiteRepository : IRepository
    {
        NotesContext db;

        public SQLiteRepository(NotesContext context)
        {
            db = context;
        }

        public async Task<IEnumerable<Note>> GetAllNotes(Guid? groupId)
        {
            if(groupId == null) return await db.Notes.ToListAsync();
            else return await db.Notes.Where(n => n.GroupId == groupId).ToListAsync();
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

        public async Task DeleteNote(Guid id)
        {

            Note? note = await db.Notes.FirstOrDefaultAsync(n => n.Id == id);
            if (note != null)
            {
                db.Notes.Remove(note);
                await db.SaveChangesAsync();
            }
        }

        public async Task EditNote(Note note)
        {
            NoteGroup? group = db.Groups.FirstOrDefault(g => g.Id == note.GroupId);
            note.Group = group;

            if (!db.Notes.Any(n => n.Id == note.Id))
            {
                note.CreateDate = DateTime.Now;
                note.EditDate = DateTime.Now;
                db.Notes.Add(note);
            }
            else
            {
                Note editNote = db.Notes.FirstOrDefault(n => n.Id == note.Id)!;
                editNote.Title = note.Title;
                editNote.Text = note.Text;
                editNote.Group = group;
                editNote.EditDate = DateTime.Now;
            }

            await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<NoteGroup>> GetAllGroups()
        {
            return await db.Groups.ToListAsync();
        }

        public async Task AddGroup(NoteGroup addedGroup)
        {
            db.Groups.Add(addedGroup);
            await db.SaveChangesAsync();
        }

        public async Task DeleteGroup(Guid id)
        {

            NoteGroup? group = await db.Groups.FirstOrDefaultAsync(g => g.Id == id);
            if (group != null)
            {
                db.Groups.Remove(group);
                await db.SaveChangesAsync();
            }
        }
    }
}
