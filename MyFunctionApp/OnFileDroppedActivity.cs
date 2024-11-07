using Microsoft.Azure.Functions.Worker;

namespace MyFunctionApp
{
    public static class OnFileDroppedActivity
    {
        [Function(nameof(OnFileDroppedActivity))]
        public static async Task RunActivity([ActivityTrigger] string subject, CancellationToken token)
        {
            // Execute the operation in background by calling my application layer
            await Task.CompletedTask;
        }
    }
}
