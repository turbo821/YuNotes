using Microsoft.AspNetCore.Mvc;
using YuNotes.Models;

namespace YuNotes.Components
{
    public class GroupMenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IEnumerable<NoteGroup> groups)
        {
            return View(groups);
        }
    }
}
