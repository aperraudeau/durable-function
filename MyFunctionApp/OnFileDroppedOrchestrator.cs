using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;

namespace MyFunctionApp
{
    public static class OnFileDroppedOrchestrator
    {
        [Function(nameof(OnFileDroppedOrchestrator))]
        public static async Task RunOrchestrator([OrchestrationTrigger] TaskOrchestrationContext context, string subject)
        {
            await context.CallActivityAsync(nameof(OnFileDroppedActivity), subject);
        }
    }
}
