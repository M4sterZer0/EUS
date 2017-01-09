using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTANetworkServer;
using GTANetworkShared;
using Datenbank;
using System.Timers;

namespace EuS
{
    public class EUS : Script
    {
        // Datenbank
        Db db = new Db();
        //Spieler
        public static List<Player> Players = new List<Player>();

        public EUS()
        {
            //Events
            var Events = new Events();
            API.onResourceStart += Events.onResourceStart;
            API.onClientEventTrigger += Events.OnClientEvent;
            API.onPlayerConnected += Events.OnClientConnected;
            API.onPlayerFinishedDownload += Events.OnPlayerFinishedDownload;
            API.onPlayerDisconnected += Events.OnClientDisconnected;

            //Timer
            var clockTimer = new System.Timers.Timer(30000);
            clockTimer.Elapsed += clockTimer_Elapsed;
            clockTimer.Start();          
        }

        public void clockTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            API.setTime(DateTime.Now.Hour, DateTime.Now.Minute);
        }

        //Befehle
        [Command("test")]
        public void command_test(Client player)
        {
            API.sendChatMessageToPlayer(player, string.Format("Dein Name laut Accountsystem ist: {0}", Player.getPlayerAccount(player).userName));
        }
    }
}
