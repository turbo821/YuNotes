using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text.RegularExpressions;
using YuNotes.Data;
using YuNotes.Models;
using YuNotes.Repositories;

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
        public async Task<IActionResult> GetAllNotes(Guid? groupId)
        {
            IEnumerable<Note> notes = await repo.GetAllNotes(groupId);
            IEnumerable<NoteGroup> groups = await repo.GetAllGroups();
            CatalogViewModel viewModel = new CatalogViewModel() { Notes = notes, NoteGroups = groups };

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

            return RedirectToAction("GetAllNotes");
        }

        [HttpPost]
        [Route("note/edit")]
        public async Task<IActionResult> EditNote(NoteViewModel model)
        {
            Note note = model.Note;
            await repo.EditNote(note);

            return RedirectToAction("GetAllNotes");
        }

        [HttpPost]
        [Route("note/addgroup")]
        public async Task<IActionResult> AddGroup(NoteGroup addedGroup)
        {

            await repo.AddGroup(addedGroup);

            return RedirectToAction("GetAllNotes");
        }

        [HttpGet]
        [Route("note/deletegroup")]
        public async Task<IActionResult> DeleteGroup(Guid id)
        {

            await repo.DeleteGroup(id);

            return RedirectToAction("GetAllNotes");
        }
    }
}
