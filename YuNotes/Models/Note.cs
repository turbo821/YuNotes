﻿using System.Text.RegularExpressions;

namespace YuNotes.Models
{
    public class Note : BaseModel
    {
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public Guid? GroupId { get; set; }
        public NoteGroup? Group { get; set; }
    }
}
