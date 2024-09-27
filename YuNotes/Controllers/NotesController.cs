using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text.RegularExpressions;
using YuNotes.Data;
using YuNotes.Models;
using YuNotes.Repositories;
using YuNotes.ViewModels;

namespace YuNotes.Controllers
{
    public class NotesController : Controller
    {
        IRepository repo;

        public NotesController(IRepository context)
        {
            repo = context;
        }


        [HttpGet]
        [Route("/")]
        [Route("notes")]
        public async Task<IActionResult> Catalog(Guid? groupid, string? title, SortState sortOrder = SortState.EditDesc)
        {
            IEnumerable<Note> notes = await repo.GetAllNotes(groupid, title);
            IEnumerable<NoteGroup> groups = await repo.GetAllGroups();

            notes = sortOrder switch
            {
                SortState.TitleDesc => notes.OrderByDescending(n => n.Title),
                SortState.EditAsc => notes.OrderBy(s => s.EditDate),
                SortState.EditDesc => notes.OrderByDescending(s => s.EditDate),
                SortState.CreateAsc => notes.OrderBy(s => s.CreateDate),
                SortState.CreateDesc => notes.OrderByDescending(s => s.CreateDate),
                _ => notes.OrderBy(s => s.Title),
            };

            CatalogViewModel viewModel = new CatalogViewModel() { Notes = notes, NoteGroups = groups, SortViewModel = new SortViewModel(sortOrder), Title = title };

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
