using Microsoft.AspNetCore.Mvc;
using YuNotes.Models;

namespace YuNotes.Repositories
{
    public interface IRepository
    {
        Task GetAllNotes();
        Task<Note> GetNote(Guid id);
        Task<Note> DeleteNote(Guid id);
        Task<Note> EditNote(Note note);
        Task<NoteGroup> AddGroup(NoteGroup addedGroup);
    }
}
