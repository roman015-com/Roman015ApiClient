using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roman015API.Clients
{
    public class StarWarsHub
    {
        private static HubConnection hubConnection;
        private static StarWarsHub starWarsHub;

        public event Action<Exception> Closed;
        public event Action<int, int> OnOrder66Executed;

        private StarWarsHub() { }

        public static StarWarsHub GetSingletonInstance()
        {
            if (starWarsHub == null)
            {
                starWarsHub = new StarWarsHub();
            }

            if (hubConnection == null)
            {
                hubConnection = new HubConnectionBuilder()
                  .WithUrl("https://api.roman015.com/StarWarsHub")
                  .WithAutomaticReconnect()
                  .Build();

                hubConnection.Closed += ((arg) =>
                {
                    starWarsHub.Closed?.Invoke(arg);
                    return hubConnection.StartAsync();
                });
                hubConnection.On<int, int>("Order66Executed", (jedi, sith) =>
                {
                    starWarsHub.OnOrder66Executed?.Invoke(jedi, sith);
                });
            }            

            return starWarsHub;
        }

        public async void JoinSide(bool isJedi)
        {
            await hubConnection.SendAsync("JoinSide", isJedi);
        }

        public async void StartConnection()
        {
            await hubConnection.StartAsync();
        }
    }
}
