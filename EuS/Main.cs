using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTANetworkServer;
using GTANetworkShared;
using Datenbank;

namespace EuS
{
    public class EUS : Script
    {
        // Datenbank
        Db db = new Db();
        public EUS()
        {
            var Events = new Events();
            API.onResourceStart += Events.onResourceStart;
            API.onClientEventTrigger += Events.OnClientEvent;
            API.onPlayerConnected += Events.OnClientConnected;
            API.onPlayerFinishedDownload += Events.OnPlayerFinishedDownload;
            API.onPlayerDisconnected += Events.OnClientDisconnected;
        }
    }
}
