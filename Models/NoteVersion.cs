namespace NotAlmaApp.Models
{
    public class NoteVersion
    {
        public int VersionId { get; set; }
        public int NoteId { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}