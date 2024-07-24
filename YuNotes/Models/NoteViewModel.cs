using Microsoft.AspNetCore.Mvc.Rendering;

namespace YuNotes.Models
{
    public class NoteViewModel
    {
        public Note Note { get; set; } = null!;
        public SelectList NoteGroups { get; set; } = new SelectList(new List<NoteGroup>(), "Id", "Name");
    }
}
