using System.Text.RegularExpressions;

namespace YuNotes.Models
{
    public class Note : BaseModel
    {
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; }
        public DateTime EditDate { get; set; }
        public Guid? GroupId { get; set; }
        public NoteGroup? Group { get; set; }

        public string ViewEditDate()
        {
            return EditDate.ToLongTimeString();
        }
        public string ViewCreateDate()
        {
            return CreateDate.ToLongTimeString();
        }
    }
}
