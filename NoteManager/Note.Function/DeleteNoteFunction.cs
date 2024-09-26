using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Note.Function.Repository;

namespace Note.Function
{
    public class DeleteNoteFunction
    {
        private readonly INoteRepository _noteRepository;

        public DeleteNoteFunction(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        [FunctionName("DeleteNote")]
        public async Task<IActionResult> DeleteNote(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "notes/{id}")] HttpRequest req, string id,
            ILogger log)
        {

            await _noteRepository.DeleteAsync(id);
            return new OkResult();
        }
    }
}
