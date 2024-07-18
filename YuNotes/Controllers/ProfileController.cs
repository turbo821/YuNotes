using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YuNotes.Data;
using YuNotes.Models;

namespace YuNotes.Controllers
{
    public class ProfileController : Controller
    {
        NotesContext db;

        public ProfileController(NotesContext context)
        {
            this.db = context;
        }


        [HttpGet]
        [Route("/")]
        [Route("notes")]
        public async Task<IActionResult> Notes()
        {
            return View(await db.Notes.ToListAsync());
        }

        [HttpGet]
        [Route("note/{id:guid}")]
        public async Task<IActionResult> Note(Guid id)
        {
            NoteModel? note;
            if (db.Notes.Any(n => n.Id == id))
            {
                note = await db.Notes.FirstOrDefaultAsync(n => n.Id == id);
            }
            else
                note = new() { Id = id };

            return View(note);
        }

        [HttpPost]
        [Route("note/delete")]
        public async Task<IActionResult> DeleteNote(Guid id)
        {

            NoteModel? note = await db.Notes.FirstOrDefaultAsync(n => n.Id == id);
            if (note != null)
            {
                db.Notes.Remove(note);
                await db.SaveChangesAsync();
                return RedirectToAction("Notes");
            }
            
            return NotFound();

        }

        [HttpPost]
        [Route("note/edit")]
        public async Task<IActionResult> EditNote(NoteModel note)
        {
            if (!db.Notes.Any(n => n.Id == note.Id))
            {
                db.Notes.Add(note);
            }
            else
            {
                db.Notes.Update(note);
            }

            await db.SaveChangesAsync();
            return RedirectToAction("Notes");
        }

    }
}
