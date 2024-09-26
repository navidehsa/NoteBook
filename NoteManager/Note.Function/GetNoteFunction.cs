using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Note.Function.Repository;

namespace Note.Function
{
    public class GetNoteFunction
    {
        private readonly INoteRepository _noteRepository;

        public GetNoteFunction(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        [FunctionName("GetNote")]
        public async Task<IActionResult> GetNote(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "notes/{id}")] HttpRequest req, string id,
            ILogger log)
        {

            var notes = await _noteRepository.GetAsync(id);

            return new OkObjectResult(notes);
        }
    }
}
