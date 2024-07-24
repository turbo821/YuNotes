
namespace YuNotes.Models
{
    public class CatalogViewModel
    {
        public IEnumerable<Note> Notes { get; set; } = new List<Note>();
        public IEnumerable<NoteGroup> NoteGroups { get; set; } = new List<NoteGroup>();
        public NoteGroup AddedGroup { get; set; } = new();
    }
}
