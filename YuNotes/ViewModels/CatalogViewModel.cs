using YuNotes.Models;

namespace YuNotes.ViewModels
{
    public class CatalogViewModel
    {
        public IEnumerable<Note> Notes { get; set; } = new List<Note>();
        public IEnumerable<NoteGroup> NoteGroups { get; set; } = new List<NoteGroup>();
        public NoteGroup AddedGroup { get; set; } = new();
        public SortViewModel SortViewModel { get; set; } = new SortViewModel(SortState.EditDesc);
    }
}
