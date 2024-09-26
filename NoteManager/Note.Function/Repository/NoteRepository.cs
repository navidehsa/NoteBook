using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Note.Function.Configuration;

namespace Note.Function.Repository
{
    public class NoteRepository : INoteRepository
    {
        private readonly CosmosClient _cosmosClient;
        private readonly Container _container;
        private readonly ICosmosDatabaseConfiguration _configuration;

        public NoteRepository(CosmosClient cosmosClient, ICosmosDatabaseConfiguration configuration)
        {
            _cosmosClient = cosmosClient;
            _configuration = configuration;
            CreateDatabaseIfNotExistsAsync(configuration.DatabaseName).Wait();
            _container = GetContainer(configuration.DatabaseName, configuration.ContainerName).Result;
        }

        public async Task<IEnumerable<Entities.PersonalNote>> GetAsync()
        {
            var query = _container.GetItemQueryIterator<Entities.PersonalNote>(new QueryDefinition("SELECT * FROM c"));
            var results = new List<Entities.PersonalNote>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task<Entities.PersonalNote> GetAsync(string id)
        {
            var response = await _container.ReadItemAsync<Entities.PersonalNote>(id, new PartitionKey(id));
            return response.Resource;
        }

        public async Task<Entities.PersonalNote> CreateAsync(Entities.PersonalNote note)
        {
            var response = await _container.CreateItemAsync(note, new PartitionKey(note.Id));
            return response.Resource;
        }

        public async Task<Entities.PersonalNote> UpdateAsync(string id, Entities.PersonalNote note)
        {
            var response = await _container.UpsertItemAsync(note, new PartitionKey(id));
            return response.Resource;
        }

        public Task DeleteAsync(string id)
        {
            return _container.DeleteItemAsync<Entities.PersonalNote>(id, new PartitionKey(id));
        }

        private async Task CreateDatabaseIfNotExistsAsync(string databaseName)
        {
            await _cosmosClient.CreateDatabaseIfNotExistsAsync(databaseName);
        }

        private async Task<Container> GetContainer(string databaseName, string containerName)
        {
            Database database = await _cosmosClient.GetDatabase(databaseName).ReadAsync();
            Container container = await database.CreateContainerIfNotExistsAsync(containerName, "/id"); 

            return container;
        }
    }
}
