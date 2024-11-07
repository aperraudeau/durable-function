using Microsoft.DurableTask;
using Moq;
using System.Reactive.Subjects;

namespace MyFunctionApp.Tests
{
    public class OnFileDroppedOrchestratorTests
    {
        [Fact]
        public async Task RunOrchestrator_SouldCallActivity()
        {
            // Arrange
            var context = new Mock<TaskOrchestrationContext>();
            var subject = "test.txt";

            // Act
            await OnFileDroppedOrchestrator.RunOrchestrator(context.Object, subject);

            // Assert
            context.Verify(c => c.CallActivityAsync(nameof(OnFileDroppedActivity), subject, null), Times.Once);
        }
    }
}
