using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Note.Function.Repository;

namespace Note.Function
{
    public class GetAllNotesFunction
    {
        private readonly INoteRepository _noteRepository;

        public GetAllNotesFunction(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        [FunctionName("GetAllNotes")]
        public async Task<IActionResult> GetAllNotes(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {

            var notes = await _noteRepository.GetAsync();

            return new OkObjectResult(notes);
        }
    }
}
