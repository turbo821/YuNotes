using YuNotes.Models;

namespace YuNotes.ViewModels
{
    public class CatalogViewModel
    {
        public IEnumerable<Note> Notes { get; set; } = new List<Note>();
        public GroupViewModel GroupModel { get; set; } = new();
        public SortViewModel SortViewModel { get; set; } = new SortViewModel(SortState.EditDesc);
        public string? SearchTitle { get; set; }
    }
}
