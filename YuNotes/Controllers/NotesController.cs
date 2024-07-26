using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text.RegularExpressions;
using YuNotes.Data;
using YuNotes.Models;

namespace YuNotes.Controllers
{
    public class NotesController : Controller
    {
        NotesContext db;

        public NotesController(NotesContext context)
        {
            db = context;
        }


        [HttpGet]
        [Route("/")]
        [Route("notes")]
        public async Task<IActionResult> GetAllNotes()
        {
            IEnumerable<Note> notes = await db.Notes.ToListAsync();
            IEnumerable<NoteGroup> groups = await db.Groups.ToListAsync();
            CatalogViewModel viewModel = new CatalogViewModel() { Notes = notes, NoteGroups = groups };

            return View(viewModel);
        }

        [HttpGet]
        [Route("note/{id:guid}")]
        public async Task<IActionResult> GetNote(Guid id)
        {
            Note note;
            if (db.Notes.Any(n => n.Id == id))
            {
                note = await db.Notes.FirstOrDefaultAsync(n => n.Id == id);
            }
            else
                note = new() { Id = id };

            SelectList noteGroups = new SelectList(await db.Groups.ToListAsync(), "Id", "Name");

            return View(new NoteViewModel() { Note = note, NoteGroups = noteGroups });
        }

        [HttpPost]
        [Route("note/delete")]
        public async Task<IActionResult> DeleteNote(Guid id)
        {

            Note? note = await db.Notes.FirstOrDefaultAsync(n => n.Id == id);
            if (note != null)
            {
                db.Notes.Remove(note);
                await db.SaveChangesAsync();
                ;
            }

            return RedirectToAction("GetAllNotes");
        }

        [HttpPost]
        [Route("note/edit")]
        public async Task<IActionResult> EditNote(NoteViewModel model)
        {
            Note note = model.Note;
            NoteGroup? group = db.Groups.FirstOrDefault(g => g.Id == note.GroupId);
            note.Group = group;

            if (!db.Notes.Any(n => n.Id == note.Id))
            {

                db.Notes.Add(note);
            }
            else
            {
                db.Notes.Update(note);
            }

            await db.SaveChangesAsync();
            return RedirectToAction("GetAllNotes");
        }

        [HttpPost]
        [Route("note/addgroup")]
        public async Task<IActionResult> AddGroup(NoteGroup addedGroup)
        {

            db.Groups.Add(addedGroup);
            await db.SaveChangesAsync();

            return RedirectToAction("GetAllNotes");
        }
    }
}
