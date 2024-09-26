using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Note.Function.Repository;

namespace Note.Function
{
    public class SearchNotesFunction
    {
        private readonly INoteRepository _noteRepository;

        public SearchNotesFunction(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        [FunctionName("SearchNotes")]
        public async Task<IActionResult> SearchNotes(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "search/{query}")] HttpRequest req, string query,
            ILogger log)
        {

            var notes = await _noteRepository.GetAsync();
            var filteredNotes = notes.Where(n => n.Title.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                                                 n.Content.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();

            return new OkObjectResult(filteredNotes);
        }
    }
}
