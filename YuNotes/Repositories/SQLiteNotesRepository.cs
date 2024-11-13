using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YuNotes.Data;
using YuNotes.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace YuNotes.Repositories
{
    public class SQLiteNotesRepository : INotesRepository
    {
        NotesContext db;

        public SQLiteNotesRepository(NotesContext context)
        {
            db = context;
        }


        public async Task<User> GetUser(string email)
        {
            User? user = await db.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user is not null)
                return user;

            else throw new NullReferenceException();
        }
        public async Task<IEnumerable<Note>> GetAllNotes(User user, Guid? groupId, string? title)
        {
            IQueryable<Note> query;
            if (groupId == null)
                query = db.Notes.Where(n => n.User == user).Include(n => n.Group).AsNoTracking();
            else
                query = db.Notes.Where(n => n.User == user).Where(n => n.GroupId == groupId)
                    .Include(n => n.Group).AsNoTracking();

            if(title == null)
                return await query.ToListAsync();
            else
            {
                List<Note> notes = await query.ToListAsync();

                return notes.Where(n => n.Title.ToUpper().Contains(title.ToUpper(), StringComparison.CurrentCultureIgnoreCase));
            }
                

        }

        public async Task<Note> GetNote(Guid id, User user)
        {
            Note? note;
            if (db.Notes.Any(n => n.Id == id && n.User == user))
            {
                note = await db.Notes.FindAsync(id);
                
                if(note is null)
                {
                    note = new() { Id = id, User = user };
                }
            }
            else
                note = new() { Id = id, User = user };

            return note;
        }

        public async Task DeleteNote(Guid id, User user)
        {
            Note? note = await db.Notes.FirstOrDefaultAsync(n => n.Id == id && n.User == user);
            if (note is not null)
            {
                db.Notes.Remove(note);
                await db.SaveChangesAsync();
            }
        }

        public async Task EditNote(Note note, User user)
        {
            NoteGroup? group = db.Groups.FirstOrDefault(g => g.Id == note.GroupId);

            if (!db.Notes.Any(n => n.Id == note.Id))
            {
                note.CreateDate = DateTime.Now;
                note.EditDate = DateTime.Now;
                note.Group = group;
                note.User = user;
                db.Notes.Add(note);
            }
            else
            {
                Note editNote = db.Notes.FirstOrDefault(n => n.Id == note.Id && n.User == user)!;

                if (editNote == note)
                {
                    return;
                }

                if (editNote.Title != note.Title) editNote.Title = note.Title;
                if (editNote.Text != note.Text) editNote.Text = note.Text;
                if (editNote.GroupId != note.GroupId)
                {
                    editNote.GroupId = note.GroupId;
                    editNote.Group = group;
                }
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
