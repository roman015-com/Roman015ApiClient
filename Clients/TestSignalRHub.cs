using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roman015API.Clients
{
    public class TestSignalRHub
    {
        private static HubConnection hubConnection;
        private static TestSignalRHub testSignalRHub;

        private TestSignalRHub() { }

        public static TestSignalRHub GetSingletonInstance()
        {
            if (testSignalRHub == null)
            {
                testSignalRHub = new TestSignalRHub();
            }

            if (hubConnection == null)
            {
                hubConnection = new HubConnectionBuilder()
                  .WithUrl("https://api.roman015.com/NotificationHub")
                  .WithAutomaticReconnect()
                  .Build();

                hubConnection.Closed += ((arg) =>
                {
                    testSignalRHub.Closed?.Invoke(arg);
                    return hubConnection.StartAsync();
                });
                hubConnection.On<string>("TestMessage", message =>
                {
                    testSignalRHub.TestMessage?.Invoke(message);
                });
            }            

            return testSignalRHub;
        }

        public event Action<Exception> Closed;
        public event Action<string> TestMessage;
        public async void StartConnection()
        {
            await hubConnection.StartAsync();
        }
    }
}
