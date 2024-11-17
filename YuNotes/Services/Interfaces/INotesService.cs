using Microsoft.AspNetCore.Mvc;
using YuNotes.Contracts;
using YuNotes.Models;
using YuNotes.ViewModels;

namespace YuNotes.Services.Interfaces
{
    public interface INotesService
    {
        Task<CatalogViewModel> GetPaginationNotes(CatalogRequest request, string userEmail);
        Task<NoteViewModel> GetNote(Guid id, string userEmail);
        Task DeleteNote(Guid id, string userEmail);
        Task EditNote(Note note, string userEmail);
        Task AddGroup(NoteGroup addedGroup, string userEmail);
        Task DeleteGroup(Guid id, string userEmail);
    }
}
