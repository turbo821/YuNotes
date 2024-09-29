using YuNotes.Models;

namespace YuNotes.ViewModels
{
    public class GroupViewModel
    {
        public NoteGroup AddedGroup { get; set; } = new();
        public IEnumerable<NoteGroup> NoteGroups { get; set; } = new List<NoteGroup>();
        public Guid? GroupId { get; set; }

    }
}
