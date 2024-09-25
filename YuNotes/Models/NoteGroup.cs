namespace YuNotes.Models
{
    public class NoteGroup : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<Note>? Notes { get; set;}
    }
}
