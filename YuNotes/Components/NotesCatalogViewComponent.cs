using Microsoft.AspNetCore.Mvc;
using YuNotes.Models;

namespace YuNotes.Components
{
    public class NotesCatalogViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IEnumerable<Note> notes)
        {
            return View(notes);
        }
    }
}
