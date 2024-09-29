using Microsoft.AspNetCore.Mvc.Rendering;
using YuNotes.Models;

namespace YuNotes.ViewModels
{
    public class NoteViewModel
    {
        public Note Note { get; set; } = null!;
        public SelectList NoteGroups { get; set; } = new SelectList(new List<NoteGroup>(), "Id", "Name");
    }
}
