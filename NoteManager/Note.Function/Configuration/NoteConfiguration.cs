using Microsoft.Extensions.Configuration;

namespace Note.Function.Configuration
{
    public class NoteConfiguration : ICosmosDatabaseConfiguration
    {
        private readonly IConfiguration _configuration;

        public NoteConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string DatabaseName => _configuration["COSMOS_DATABASE_NAME"] ?? "";
        public string ContainerName => _configuration["DATABASE_CONTAINER_NAME"] ?? "";
        public string ConnectionString => _configuration["COSMOS_CONNECTION_STRING"] ?? "";
    }
}
