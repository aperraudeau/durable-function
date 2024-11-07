using Microsoft.DurableTask.Client.Entities;
using Microsoft.DurableTask.Client;
using Moq;
using Microsoft.Extensions.Logging;
using Azure.Messaging.EventGrid;

namespace MyFunctionApp.Tests
{
    public class OnFileDroppedTests
    {
        private readonly Mock<ILogger<OnFileDropped>> _loggerMock;
        private readonly OnFileDropped _function;

        public OnFileDroppedTests()
        {
            _loggerMock = new Mock<ILogger<OnFileDropped>>();
            _function = new OnFileDropped(_loggerMock.Object);
        }

        [Fact]
        public async Task OnFileDropped_WhenIsTriggered_ShouldExecute()
        {
            // Arrange
            // Create me a mock of EventGridEvent
            var eventGridPayload = new EventGridEvent("file.txt", "file.txt", "1.0", "data");

            var durableClientMock = new Mock<DurableTaskClient>("test");
            var entityClient = new Mock<DurableEntityClient>("test");
            durableClientMock.Setup((x) => x.Entities).Returns(entityClient.Object);

            // Act
            await _function.Run(eventGridPayload, durableClientMock.Object, default);

            // Assert
            durableClientMock.Verify(client => client.ScheduleNewOrchestrationInstanceAsync(
                nameof(OnFileDroppedOrchestrator), "file.txt", default), Times.Once);
        }
    }
}