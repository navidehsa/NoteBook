namespace Note.Function.Configuration
{
    public interface ICosmosDatabaseConfiguration
    {
        public string DatabaseName { get; }
        public string ContainerName { get; }
        public string ConnectionString { get; }
    }
}
