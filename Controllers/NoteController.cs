using Microsoft.AspNetCore.Mvc;
using NotAlmaApp.Models;


namespace NotAlmaApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NoteController : ControllerBase
    {
        //in-memory not tutacagız
        private static List<Note> notes = new List<Note>();
        private static int nextId = 1;

        // Her not güncellendiğinde eski versiyonları saklamak için liste
        private static List<NoteVersion> versions = new List<NoteVersion>();

        // VersionId üretmek için sayaç
        private static int nextVersionId = 1;


        
        [HttpGet]
        public ActionResult<List<Note>> GetNotes() //metod başarılı olursa geriye notların listesini döndürecek.
        {
            return Ok(notes);
        }


        [HttpPost]
        public ActionResult<Note> CreateNote //Eğer işlem başarılı olursa geriye eklenen not nesnesi döner.
        ([FromBody] Note note)
        //[FromBody] notun HTTP isteğinin body kısmından gelmesini sağlar. Yani client, not verisini JSON formatında gönderir ve bu veri Note nesnesine dönüştürülür. Bu sayede API, gelen veriyi doğru şekilde alır ve işler.

        {
           
            note.Id = nextId++;

            
            notes.Add(note);

            // Not oluşturulduğunda ilk version da kaydediyoruz
            var version = new NoteVersion
            {
                VersionId = nextVersionId++,
                NoteId = note.Id,
                Title = note.Title,
                Content = note.Content
            };

            versions.Add(version);

            return CreatedAtAction(nameof(GetNotes), new { id = note.Id }, note);
        }


        [HttpPut("{id}")]
        public ActionResult UpdateNote(int id, [FromBody] Note note)
        {
            var existingNote = notes.FirstOrDefault(n => n.Id == id);

            if (existingNote == null)
                return NotFound();

            existingNote.Title = note.Title;
            existingNote.Content = note.Content;

            // Her güncellemede yeni bir versiyon kaydediyoruz
            var version = new NoteVersion
            {
                VersionId = nextVersionId++,
                NoteId = id,
                Title = note.Title,
                Content = note.Content
            };

            versions.Add(version);

            return Ok(existingNote);
        }


        [HttpGet("{id}/versions")]
        public ActionResult GetVersions(int id)
        {
            var noteVersions = versions.Where(v => v.NoteId == id).ToList();

            return Ok(noteVersions);
        }


        [HttpDelete("{id}")]
        public ActionResult DeleteNote(int id)
        {
            var note = notes.FirstOrDefault(n => n.Id == id);

            if (note == null) return NotFound();

            notes.Remove(note);

            return NoContent();
        }
    }
}