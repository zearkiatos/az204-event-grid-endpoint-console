using dotenv.net;

namespace Configuration
{
    public static class AppConfiguration
    {
        private static IDictionary<string, string> envVars = null;
        static AppConfiguration()
        {
            DotEnv.Load();
            envVars = DotEnv.Read();
        }

        public static string TopicEndpoint => 
            envVars["TOPIC_ENDPOINT"] 
            ?? throw new InvalidOperationException("TOPIC_ENDPOINT environment variable is not set");

        public static string TopicAccessKey => 
            envVars["TOPIC_ACCESS_KEY"] 
            ?? throw new InvalidOperationException("TOPIC_ACCESS_KEY environment variable is not set");

        public static void ValidateConfiguration()
        {
            try
            {
                _ = TopicEndpoint;
                _ = TopicAccessKey;
                Console.WriteLine("✓ Configuration validation passed");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"✗ Configuration validation failed: {ex.Message}");
                throw;
            }
        }
    }
}