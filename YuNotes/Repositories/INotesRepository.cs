using Microsoft.AspNetCore.Mvc;
using YuNotes.Models;

namespace YuNotes.Repositories
{
    public interface INotesRepository
    {
        Task<IEnumerable<Note>> GetAllNotes(Guid? groupId, string? title);
        Task<Note> GetNote(Guid id);
        Task DeleteNote(Guid id);
        Task EditNote(Note note);
        Task<IEnumerable<NoteGroup>> GetAllGroups();
        Task AddGroup(NoteGroup addedGroup);
        Task DeleteGroup(Guid id);
    }
}
