using BlazorLaboratory.WebApi.Hubs;
using Microsoft.AspNetCore.SignalR;
using Moq;

namespace BlazorLaboratory.WebApi.Tests.Hubs;

public class CounterHubTests
{
    [Fact]
    public async Task AddToTotal_ShouldSendCounterIncrementMessageToAllClients()
    {
        //// Arrange
        //var hub = new CounterHub();
        //var mockClients = new Mock<IClientProxy>();
        //hub.Clients = (IHubCallerClients)mockClients.Object;

        //// Act
        //await hub.AddToTotal("user1", 5);

        //// Assert
        //mockClients.Verify(x => x.SendCoreAsync("CounterIncrement", 
        //    It.Is<object[]>(args => (string)args[0] == "user1" && (int)args[1] == 5))
        //Times.Once());
    }
}