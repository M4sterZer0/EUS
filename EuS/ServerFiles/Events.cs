using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTANetworkServer;
using GTANetworkShared;
using EuS;
using Datenbank;

namespace EuS
{
    public class Events : Script
    {
        Db db = new Db();
        public void onResourceStart()
        {
            //Ingame-Uhrzeit zum Start auf die aktuelle Uhrzeit setzen
            API.setTime(DateTime.Now.Hour, DateTime.Now.Minute);
        }

        public void OnClientEvent(Client sender, string eventName, object[] arguments)
        {
            if (eventName == "closeLogin")
            {
                API.sendChatMessageToPlayer(sender, string.Format("Username: {0} Passwort: {1}", arguments[0].ToString(), arguments[1].ToString()));
                db.bind("username", arguments[0].ToString());
                db.bind("passwort", arguments[1].ToString());
                string[] details = db.row("SELECT * FROM User WHERE Name = @username AND Passwort = @passwort");
                API.sendChatMessageToPlayer(sender, "Existiert?: " + details.Length);
                if (details.Length == 0)
                {
                    //Account existiert nicht
                } else
                {
                    var tmpPlayer = Player.getPlayerAccount(sender);
                    tmpPlayer.userName = details.GetValue(1).ToString().Replace(".", " ").Replace("_", " ");
                    tmpPlayer.userBank = Convert.ToInt32(details.GetValue(3));
                    tmpPlayer.userCar = Convert.ToInt32(details.GetValue(4));
                    tmpPlayer.userMoney = Convert.ToInt32(details.GetValue(5));
                    tmpPlayer.userSkin = details.GetValue(6).ToString();
                    tmpPlayer.userHouse = Convert.ToInt32(details.GetValue(7));
                    tmpPlayer.userFrakID = Convert.ToInt32(details.GetValue(8));
                    tmpPlayer.userTelNr = Convert.ToInt32(details.GetValue(9));
                    tmpPlayer.userLastLogin = Convert.ToInt32(details.GetValue(10));
                    tmpPlayer.userRegister = Convert.ToInt32(details.GetValue(11));
                    tmpPlayer.userTS3ID = details.GetValue(12).ToString();
                    tmpPlayer.userForumID = Convert.ToInt32(details.GetValue(13));
                    tmpPlayer.userPremium = Convert.ToInt32(details.GetValue(14));
                    tmpPlayer.userPremiumTime = Convert.ToInt32(details.GetValue(15));
                    tmpPlayer.userAdminRank = Convert.ToInt32(details.GetValue(16));
                    tmpPlayer.userRegIP = details.GetValue(17).ToString();
                    tmpPlayer.userLastIP = details.GetValue(18).ToString();
                    tmpPlayer.userBan = Convert.ToInt32(details.GetValue(19));
                    tmpPlayer.userWarns = Convert.ToInt32(details.GetValue(20));
                    tmpPlayer.userLogoutX = float.Parse(details.GetValue(21).ToString());
                    tmpPlayer.userLogoutY = float.Parse(details.GetValue(22).ToString());
                    tmpPlayer.userLogoutZ = float.Parse(details.GetValue(23).ToString());
                    tmpPlayer.userAduty = Convert.ToInt32(details.GetValue(24));
                    tmpPlayer.userFrakrank = Convert.ToInt32(details.GetValue(25));
                    tmpPlayer.userFrakDuty = Convert.ToInt32(details.GetValue(26));
                    tmpPlayer.userWanteds = Convert.ToInt32(details.GetValue(27));
                    tmpPlayer.userJobID = Convert.ToInt32(details.GetValue(28));
                    tmpPlayer.userJobDuty = Convert.ToInt32(details.GetValue(29));
                    tmpPlayer.userLoggedin = true;
                    API.sendNativeToPlayer(sender, HashFunctions.DO_SCREEN_FADE_IN, 10000);
                }
            }
        }

        public void OnClientConnected(Client player)
        {
            EUS.Players.Add(new Player(player));
            //API.sendNativeToPlayer(player, HashFunctions.DO_SCREEN_FADE_OUT, 1);
        }

        public void OnPlayerFinishedDownload(Client player)
        {
            API.triggerClientEvent(player, "showWindow", "login");
            db.bind("username", player.name);
            string[] details = db.row("SELECT * FROM User WHERE Name = @username");
            if (details.Length == 0)
            {
                API.triggerClientEvent(player, "showWindow", "register");
            } else
            {
                API.triggerClientEvent(player, "showWindow", "login");
            }
        }

        public void OnClientDisconnected(Client player, string reason)
        {
            if(Player.getPlayerAccount(player).userLoggedin)
            {
                //Speichern!
            }
            EUS.Players.Remove(Player.getPlayerAccount(player));
        }
    }
}
