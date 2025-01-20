namespace Tiltwire.Services.Shared;

public static class ServiceConfig
{
    public static void Init(string serviceName)
    {
        // General
        ServiceName = serviceName;
        
        // Postgres
        PostgresHost = Environment.GetEnvironmentVariable(APP_POSTGRES_HOST) ?? "";
        PostgresUsername = Environment.GetEnvironmentVariable(APP_POSTGRES_USERNAME) ?? "";
        PostgresPassword = Environment.GetEnvironmentVariable(APP_POSTGRES_PASSWORD) ?? "";
        // Elastic
        ElasticApiKey = Environment.GetEnvironmentVariable(APP_ELASTIC_APIKEY) ?? "";
        ElasticNodes = Environment.GetEnvironmentVariable(APP_ELASTIC_NODES) ?? "";

        Console.WriteLine($"PostgresHost={PostgresHost}");
        Console.WriteLine($"PostgresUsername={PostgresUsername}");
        Console.WriteLine($"ElasticApiKey={ElasticApiKey}");
        Console.WriteLine($"ElasticNodes={ElasticNodes}");
    }

    #region Constants

    // Postgres
    private const string APP_POSTGRES_HOST = "APP_POSTGRES_HOST";
    private const string APP_POSTGRES_USERNAME = "APP_POSTGRES_USERNAME";
    private const string APP_POSTGRES_PASSWORD = "APP_POSTGRES_PASSWORD";

    // Elastic
    private const string APP_ELASTIC_APIKEY = "APP_ELASTIC_APIKEY";
    private const string APP_ELASTIC_NODES = "APP_ELASTIC_NODES";

    #endregion

    #region Properties

    // General
    public static string ServiceName { get; set; } = string.Empty;
    
    // Postgres
    public static string PostgresHost { get; set; } = string.Empty;
    public static string PostgresUsername { get; set; } = string.Empty;
    public static string PostgresPassword { get; set; } = string.Empty;

    // Elastic
    public static string? ElasticApiKey { get; set; } = string.Empty;
    public static string ElasticNodes { get; set; } = string.Empty;

    #endregion
}