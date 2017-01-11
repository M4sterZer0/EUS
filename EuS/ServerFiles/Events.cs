using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTANetworkServer;
using GTANetworkShared;
using EuS;
using EuS.ServerFiles;
using System.Data;
using MySql.Data.MySqlClient;

namespace EuS
{
    public class Events : Script
    {
        public void onResourceStart()
        {
            //Ingame-Uhrzeit zum Start auf die aktuelle Uhrzeit setzen
            API.setTime(DateTime.Now.Hour, DateTime.Now.Minute);
        }

        public void OnClientEvent(Client sender, string eventName, object[] arguments)
        {
            if (eventName == "closeLogin")
            {
                string query = string.Format("SELECT * FROM User WHERE Name = '{0}' AND Passwort = '{1}'", MySqlHelper.EscapeString(sender.name), Useful.sha256(arguments[0].ToString()));
                DataTable result = API.exported.database.executeQueryWithResult(query);

                if (result.Rows.Count == 0)
                {
                    API.triggerClientEvent(sender, "setLoginmessage", "<span style=\"color: white;\">Das Passwort ist falsch.</span>");
                } else
                {
                    sender.position = new Vector3(-540.0245, -212.8223, 37.64985);
                    var tmpPlayer = Player.getPlayerAccount(sender);
                    tmpPlayer.userName = result.Rows[0][1].ToString().Replace(".", " ").Replace("_", " ");
                    tmpPlayer.userBank = Convert.ToInt32(result.Rows[0][3]);
                    tmpPlayer.userCar = Convert.ToInt32(result.Rows[0][4]);
                    tmpPlayer.userMoney = Convert.ToInt32(result.Rows[0][5]);
                    tmpPlayer.userSkin = result.Rows[0][6].ToString();
                    tmpPlayer.userHouse = Convert.ToInt32(result.Rows[0][7]);
                    tmpPlayer.userFrakID = Convert.ToInt32(result.Rows[0][8]);
                    tmpPlayer.userTelNr = Convert.ToInt32(result.Rows[0][9]);
                    tmpPlayer.userLastLogin = Convert.ToInt32(result.Rows[0][10]);
                    tmpPlayer.userRegister = Convert.ToInt32(result.Rows[0][11]);
                    tmpPlayer.userTS3ID = result.Rows[0][12].ToString();
                    tmpPlayer.userForumID = Convert.ToInt32(result.Rows[0][13]);
                    tmpPlayer.userPremium = Convert.ToInt32(result.Rows[0][14]);
                    tmpPlayer.userPremiumTime = Convert.ToInt32(result.Rows[0][15]);
                    tmpPlayer.userAdminRank = Convert.ToInt32(result.Rows[0][16]);
                    tmpPlayer.userRegIP = result.Rows[0][17].ToString();
                    tmpPlayer.userLastIP = result.Rows[0][18].ToString();
                    tmpPlayer.userBan = Convert.ToInt32(result.Rows[0][19]);
                    tmpPlayer.userWarns = Convert.ToInt32(result.Rows[0][20]);
                    tmpPlayer.userLogoutX = float.Parse(result.Rows[0][21].ToString());
                    tmpPlayer.userLogoutY = float.Parse(result.Rows[0][22].ToString());
                    tmpPlayer.userLogoutZ = float.Parse(result.Rows[0][23].ToString());
                    tmpPlayer.userAduty = Convert.ToInt32(result.Rows[0][24]);
                    tmpPlayer.userFrakrank = Convert.ToInt32(result.Rows[0][25]);
                    tmpPlayer.userFrakDuty = Convert.ToInt32(result.Rows[0][26]);
                    tmpPlayer.userWanteds = Convert.ToInt32(result.Rows[0][27]);
                    tmpPlayer.userJobID = Convert.ToInt32(result.Rows[0][28]);
                    tmpPlayer.userJobDuty = Convert.ToInt32(result.Rows[0][29]);
                    tmpPlayer.userLoggedin = true;

                    API.setPlayerSkin(sender, API.pedNameToModel(tmpPlayer.userSkin));
                    API.triggerClientEvent(sender, "hideWindow", "login");
                    API.sendNativeToPlayer(sender, HashFunctions.DO_SCREEN_FADE_IN, 10000);
                    API.freezePlayer(sender, false);
                }
            }
        }

        public void OnClientConnected(Client player)
        {
            EUS.Players.Add(new Player(player));
            API.freezePlayer(player, true);
            API.sendNativeToPlayer(player, HashFunctions.DO_SCREEN_FADE_OUT, 1);
        }

        public void OnPlayerFinishedDownload(Client player)
        {
            API.triggerClientEvent(player, "showWindow", "login");
            string query = string.Format("SELECT * FROM User WHERE Name = '{0}'", MySqlHelper.EscapeString(player.name));
            DataTable result = API.exported.database.executeQueryWithResult(query);

            if (result.Rows.Count == 0)
            {
                API.sendChatMessageToPlayer(player, "Account existiert nicht!");
                API.triggerClientEvent(player, "showWindow", "register");
            } else
            {
                API.sendChatMessageToPlayer(player, "Account existiert!");
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
