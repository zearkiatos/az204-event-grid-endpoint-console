using Azure.Messaging.EventGrid; 
using Configuration;
AppConfiguration.ValidateConfiguration();
ProcessAsync().GetAwaiter().GetResult();

async Task ProcessAsync()
{
    var topicEndpoint = AppConfiguration.TopicEndpoint;
    var topicKey = AppConfiguration.TopicAccessKey;
    
    if (string.IsNullOrEmpty(topicEndpoint) || string.IsNullOrEmpty(topicKey))
    {
        Console.WriteLine("Please set TOPIC_ENDPOINT and TOPIC_ACCESS_KEY in your .env file.");
        return;
    }

    EventGridPublisherClient client = new EventGridPublisherClient
        (new Uri(topicEndpoint),
        new Azure.AzureKeyCredential(topicKey));

    var eventGridEvent = new EventGridEvent(
        subject: "ExampleSubject",
        eventType: "ExampleEventType",
        dataVersion: "1.0",
        data: new { Message = "Hello, Event Grid!" }
    );

    await client.SendEventAsync(eventGridEvent);
    Console.WriteLine("Event sent successfully.");
}