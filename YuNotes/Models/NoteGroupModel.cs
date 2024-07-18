namespace YuNotes.Models
{
    public class NoteGroupModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public NoteGroupModel(string name)
        {
            Name = name;
        }
    }
}
