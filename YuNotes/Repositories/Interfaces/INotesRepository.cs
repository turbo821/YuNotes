using YuNotes.Models;

namespace YuNotes.Repositories.Interfaces
{
    public interface INotesRepository
    {
        Task<User> GetUser(string email);
        Task<IEnumerable<Note>> GetAllNotes(User user, Guid? groupId, string? title);
        Task<Note> GetNote(Guid id, User user);
        Task DeleteNote(Guid id, User user);
        Task EditNote(Note note, User user);
        Task<IEnumerable<NoteGroup>> GetAllGroups(User user);
        Task AddGroup(NoteGroup addedGroup, string userEmail);
        Task DeleteGroup(Guid id, string userEmail);
    }
}
