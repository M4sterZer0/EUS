using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTANetworkServer;
using GTANetworkShared;
using System.Timers;
using EuS.ServerFiles;
using System.IO;

namespace EuS
{
    public class EUS : Script
    {
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

        [Command("save", "/save [Name des Ortes / kurze Beschreibung]", GreedyArg = true)]
        public void Command_Pos(Client player, string name)
        {
            API.createTextLabel("Position von  ~g~" + player.name + "~w~ gespeichert, als: \"~g~" + name + "~w~\"!", player.position, 300.0f, 0.3f);
            var pos = API.getEntityPosition(player);
            var angle = API.getEntityRotation(player);
            File.AppendAllText("savepos.txt", string.Format("{0}: {1} {2} {3} {4}", name, Convert.ToDouble(pos.X).ToGBString(), Convert.ToDouble(pos.Y).ToGBString(), Convert.ToDouble(pos.Z), Convert.ToDouble(pos.X).ToGBString(), Convert.ToDouble(pos.Y).ToGBString(), Convert.ToDouble(angle).ToGBString()) + System.Environment.NewLine);
        }

    }
}
