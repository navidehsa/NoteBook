using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Note.Function.Repository;

namespace Note.Function
{
    public class UpdateNoteFunction
    {
        private readonly INoteRepository _noteRepository;

        public UpdateNoteFunction(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        [FunctionName("UpdateNote")]
        public async Task<IActionResult> UpdateNote(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "notes/{id}")] HttpRequest req, string id,
            ILogger log)
        {

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var updatedNote = JsonConvert.DeserializeObject<Entities.PersonalNote>(requestBody);
            if (updatedNote == null || updatedNote.Id != id)
            {
                return new BadRequestObjectResult("Invalid note data.");
            }

            var note = await _noteRepository.UpdateAsync(id, updatedNote);
            return new OkObjectResult(note);
        }
    }
}
