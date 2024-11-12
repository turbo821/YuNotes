using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Claims;
using System.Text.RegularExpressions;
using YuNotes.Contracts;
using YuNotes.Data;
using YuNotes.Models;
using YuNotes.Repositories;
using YuNotes.ViewModels;

namespace YuNotes.Controllers
{
    [Authorize]
    public class NotesController : Controller
    {
        INotesRepository repo;

        public NotesController(INotesRepository context)
        {
            repo = context;
        }


        [HttpGet]
        [Route("notes")]
        public async Task<IActionResult> Catalog(CatalogRequest request)
        {
            var user = HttpContext.User;
            var claims = user.Claims;
            var userEmail = user.FindFirst(ClaimTypes.Email);

            int pageSize = 4;

            IEnumerable<Note> notesQuery = await repo.GetAllNotes(request.GroupId, request.SearchTitle);
            IEnumerable<NoteGroup> groups = await repo.GetAllGroups();

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

            CatalogViewModel viewModel = new() { Notes = notes, 
                GroupModel = new() { NoteGroups = groups, GroupId = request.GroupId },
                SortViewModel = new SortViewModel(request.SortOrder),
                PageViewModel = new PageViewModel(count, request.Page, pageSize),
                SearchTitle = request.SearchTitle };

            return View(viewModel);
        }

        [HttpGet]
        [Route("note/{id:guid}")]
        public async Task<IActionResult> GetNote(Guid id)
        {
            Note note = await repo.GetNote(id);
            IEnumerable<NoteGroup> groups = await repo.GetAllGroups();

            SelectList noteGroups = new SelectList(groups, "Id", "Name");

            return View(new NoteViewModel() { Note = note, NoteGroups = noteGroups });
        }

        [HttpPost]
        [Route("note/delete")]
        public async Task<IActionResult> DeleteNote(Guid id)
        {
            await repo.DeleteNote(id);

            return RedirectToAction("Catalog");
        }

        [HttpPost]
        [Route("note/edit")]
        public async Task<IActionResult> EditNote(NoteViewModel model)
        {
            Note note = model.Note;
            await repo.EditNote(note);

            return RedirectToAction("Catalog");
        }

        [HttpPost]
        [Route("note/addgroup")]
        public async Task<IActionResult> AddGroup(NoteGroup addedGroup)
        {
            if(addedGroup.Name is null)
                RedirectToAction("Catalog");

            await repo.AddGroup(addedGroup);
            return RedirectToAction("Catalog");
        }

        [HttpGet]
        [Route("note/deletegroup")]
        public async Task<IActionResult> DeleteGroup(Guid id)
        {
            await repo.DeleteGroup(id);
            return RedirectToAction("Catalog");
        }
    }
}
