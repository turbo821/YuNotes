namespace YuNotes.Models
{
    public class Note : BaseModel
    {
        public string Title { get; set; } = string.Empty;
        public string? Text { get; set; } = string.Empty;
        public DateTime? CreateDate { get; set; } = null;
        public DateTime? EditDate { get; set; } = null;
        public Guid? GroupId { get; set; }
        public NoteGroup? Group { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public static bool operator ==(Note left, Note right)
        {
            if (left.Id == right.Id
                && left.Title == right.Title
                && left.Text == right.Text
                && left.GroupId == right.GroupId
                && left.User.Id == right.User.Id)
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
                && left.CreateDate == right.CreateDate
                && left.User.Id == right.User.Id)
            {
                return false;
            }
            return true;
        }
    }
}
