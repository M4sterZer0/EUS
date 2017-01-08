using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTANetworkServer;
using GTANetworkShared;
using EuS;

namespace EuS
{
    public class Events : Script
    {
        public void onResourceStart()
        {
            throw new NotImplementedException();
        }

        public void OnClientEvent(Client sender, string eventName, object[] arguments)
        {
            throw new NotImplementedException();
        }

        public void OnClientConnected(Client player)
        {
            EUS.Players.Add(player);
        }

        public void OnPlayerFinishedDownload(Client player)
        {
            throw new NotImplementedException();
        }

        public void OnClientDisconnected(Client player, string reason)
        {
            throw new NotImplementedException();
        }
    }
}
