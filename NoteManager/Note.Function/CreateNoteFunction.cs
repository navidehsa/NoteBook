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
    public class CreateNoteFunction
    {
        private readonly INoteRepository _noteRepository;

        public CreateNoteFunction(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        [FunctionName("CreateNote")]
        public async Task<IActionResult> CreateNote(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req)
        {
           
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var note = JsonConvert.DeserializeObject<Entities.PersonalNote>(requestBody);

            var createdNote = await _noteRepository.CreateAsync(note);

            return new OkObjectResult(createdNote);
        }
    }
}
