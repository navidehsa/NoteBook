using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Note.Function.Configuration;
using Note.Function.Repository;
using Note.Function.Services;

[assembly: FunctionsStartup(typeof(Note.Function.Startup))]

namespace Note.Function
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {

            builder.Services.AddSingleton<ICosmosDatabaseConfiguration, NoteConfiguration>();

            builder.Services.AddSingleton<CosmosClient>(sp =>
            {
                var config = sp.GetService<ICosmosDatabaseConfiguration>();
                return new CosmosClient(config.ConnectionString); 
            });

            builder.Services.AddSingleton<INoteRepository, NoteRepository>();

            builder.Services.AddSingleton<IEmailService, EmailService>();
          
        }
    }
}
