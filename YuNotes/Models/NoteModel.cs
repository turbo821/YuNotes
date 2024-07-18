using System.Text.RegularExpressions;

namespace YuNotes.Models
{
    public class NoteModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = "";
        public string Text { get; set; } = "";
        //public NoteGroupModel Group { get; set; } = null!;
    }
}
