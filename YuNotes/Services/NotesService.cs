using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using YuNotes.Constants;
using YuNotes.Contracts;
using YuNotes.Models;
using YuNotes.Repositories.Interfaces;
using YuNotes.Services.Interfaces;
using YuNotes.ViewModels;

namespace YuNotes.Services
{
    public class NotesService : INotesService
    {
        INotesRepository _repo;
        public NotesService(INotesRepository repo) 
        {
            _repo = repo;
        }

        public async Task<CatalogViewModel> GetPaginationNotes(CatalogRequest request, string userEmail)
        {
            int pageSize = Pagination.PAGE_SIZE;

            User user = await _repo.GetUser(userEmail);
            IEnumerable<Note> notesQuery = await _repo.GetAllNotes(user, request.GroupId, request.SearchTitle);
            IEnumerable<NoteGroup> groups = await _repo.GetAllGroups(user);

            notesQuery = request.SortOrder switch
            {
                SortState.TitleDesc => notesQuery.OrderByDescending(n => n.Title),
                SortState.EditAsc => notesQuery.OrderBy(s => s.EditDate),
                SortState.EditDesc => notesQuery.OrderByDescending(s => s.EditDate),
                SortState.CreateAsc => notesQuery.OrderBy(s => s.CreateDate),
                SortState.CreateDesc => notesQuery.OrderByDescending(s => s.CreateDate),
                _ => notesQuery.OrderBy(s => s.Title),
            };

            var count = notesQuery.Count();
            var notes = notesQuery.Skip((request.Page - 1) * pageSize).Take(pageSize);

            return new()
            {
                Notes = notes,
                GroupModel = new() { NoteGroups = groups, GroupId = request.GroupId },
                SortViewModel = new SortViewModel(request.SortOrder),
                PageViewModel = new PageViewModel(count, request.Page, pageSize),
                SearchTitle = request.SearchTitle
            };
        }

        public async Task<NoteViewModel> GetNote(Guid id, string userEmail)
        {
            User user = await _repo.GetUser(userEmail);
            Note note = await _repo.GetNote(id, user);
            IEnumerable<NoteGroup> groups = await _repo.GetAllGroups(user);

            SelectList noteGroups = new SelectList(groups.OrderBy(g => g.Name), "Id", "Name");

            return new NoteViewModel() { Note = note, NoteGroups = noteGroups };
        }

        public async Task DeleteNote(Guid id, string userEmail)
        {
            User user = await _repo.GetUser(userEmail);
            await _repo.DeleteNote(id, user);
        }

        public async Task EditNote(Note note, string userEmail)
        {
            User user = await _repo.GetUser(userEmail);
            await _repo.EditNote(note, user);
        }

        public async Task AddGroup(NoteGroup addedGroup, string userEmail)
        {
            await _repo.AddGroup(addedGroup, userEmail);
        }

        public async Task DeleteGroup(Guid id, string userEmail)
        {
            await _repo.DeleteGroup(id, userEmail);
        }
    }
}
