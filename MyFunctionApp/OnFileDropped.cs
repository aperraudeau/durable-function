using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.DurableTask.Client;
using Azure.Messaging.EventGrid;

namespace MyFunctionApp
{
    public class OnFileDropped(ILogger<OnFileDropped> logger)
    {
        [Function(nameof(OnFileDropped))]
        public async Task Run([EventGridTrigger] EventGridEvent eventGridEvent,
            [DurableClient] DurableTaskClient durableTaskClient, CancellationToken token)
        {
            logger.LogInformation("Received event with subject : {Subject}", eventGridEvent.Subject);

            var instanceId = await durableTaskClient.ScheduleNewOrchestrationInstanceAsync(
                nameof(OnFileDroppedOrchestrator), eventGridEvent.Subject, token);

            logger.LogInformation("Started durable task with Id = {InstanceId}", instanceId);
        }
    }
}
