using Microsoft.AspNetCore.Mvc;
using YuNotes.Models;

namespace YuNotes.Repositories
{
    public interface IRepository
    {
        Task<IEnumerable<Note>> GetAllNotes();
        Task<Note> GetNote(Guid id);
        Task DeleteNote(Guid id);
        Task EditNote(Note note);
        Task<IEnumerable<NoteGroup>> GetAllGroups();
        Task AddGroup(NoteGroup addedGroup);
    }
}
