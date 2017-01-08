using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTANetworkServer;
using GTANetworkShared;

namespace EuS
{
    public class Events : Script
    {
        internal void onResourceStart()
        {
            throw new NotImplementedException();
        }

        internal void OnClientEvent(Client sender, string eventName, object[] arguments)
        {
            throw new NotImplementedException();
        }

        internal void OnClientConnected(Client player)
        {
            throw new NotImplementedException();
        }

        internal void OnPlayerFinishedDownload(Client player)
        {
            throw new NotImplementedException();
        }

        internal void OnClientDisconnected(Client player, string reason)
        {
            throw new NotImplementedException();
        }
    }
}
