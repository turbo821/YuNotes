
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

        public static bool operator ==(Note left, Note right)
        {
            if (left.Id == right.Id
                && left.Title == right.Title
                && left.Text == right.Text
                && left.GroupId == right.GroupId)
            {
                return true;
            }
            return false;
        }

        public static bool operator !=(Note left, Note right)
        {
            if (left.Id == right.Id
                && left.Title == right.Title
                && left.EditDate == right.EditDate
                && left.CreateDate == right.CreateDate)
            {
                return false;
            }
            return true;
        }
    }
}
