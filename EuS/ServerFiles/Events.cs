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
            if(eventName == "closeRegister")
            {
                if (string.IsNullOrEmpty(arguments[0].ToString()) || string.IsNullOrEmpty(arguments[1].ToString()) || string.IsNullOrEmpty(arguments[2].ToString()) || string.IsNullOrEmpty(arguments[3].ToString()))
                {
                    API.triggerClientEvent(sender, "setRegistermessage", "<span style=\"color: white;\">Bitte f&uuml;lle alle Felder aus</span>");
                } else
                {
                    foreach (string lol in arguments)
                    {
                        Console.WriteLine(lol);
                        API.triggerClientEvent(sender, "setRegistermessage", "<span style=\"color: white;\">" + lol  + "</span>");
                    }
                    string query = string.Format("SELECT * FROM User WHERE Name = '{0}'", MySqlHelper.EscapeString(arguments[0].ToString() + "." + arguments[1].ToString()));
                    DataTable result = API.exported.database.executeQueryWithResult(query);

                    if (result.Rows.Count == 0)
                    {
                        string vorname = Useful.RemoveSpecialCharacters(Useful.ucfirst(arguments[0].ToString()));
                        string nachname = Useful.RemoveSpecialCharacters(Useful.ucfirst(arguments[1].ToString()));
                        string pass = arguments[2].ToString();
                        string passwdh = arguments[3].ToString();
                        if (vorname.Contains(" "))
                        {
                            API.triggerClientEvent(sender, "setRegistermessage", "<span style=\"color: white;\">Der Vorname enthält ung&uuml;ltige Zeichen</span>");
                        }
                        else
                        {
                            if (nachname.Contains(" "))
                            {
                                API.triggerClientEvent(sender, "setRegistermessage", "<span style=\"color: white;\">Der Vorname enth&auml;lt ung&uuml;ltige Zeichen</span>");
                            } else
                            {
                                if(pass != passwdh)
                                {
                                    API.triggerClientEvent(sender, "setRegistermessage", "<span style=\"color: white;\">Die Passw&ouml;rter stimmen nicht &uuml;berein</span>");
                                } else
                                {
                                    if(pass.Length < 8 || passwdh.Length < 8)
                                    {
                                        API.triggerClientEvent(sender, "setRegistermessage", "<span style=\"color: white;\">Das Passwort muss mind. 8 Zeichen lang sein!</span>");
                                    } else
                                    {
                                        if(vorname.Length < 3 || nachname.Length < 3)
                                        {
                                            API.triggerClientEvent(sender, "setRegistermessage", "<span style=\"color: white;\">Dein Vor- und Nachname muss mind. 3 Zeichen lang sein</span>");
                                        } else
                                        {
                                            API.triggerClientEvent(sender, "hideWindow", "register");
                                            API.triggerClientEvent(sender, "showWindow", "login");
                                            API.triggerClientEvent(sender, "setLoginmessage", "<span style=\"color: green;\">Du hast dich erfolgreich registriert</span>");
                                            query = string.Format("INSERT INTO User (Name, Passwort) VALUES ('{0}', '{1}')", MySqlHelper.EscapeString(vorname + "." + nachname), Useful.sha256(pass));
                                            API.exported.database.executeQueryWithResult(query);
                                        }
                                    }
                                }
                            }
                        }
                    } else
                    {
                        API.triggerClientEvent(sender, "setRegistermessage", "<span style=\"color: white;\">Es gibt bereits einen Nutzer mit diesem Namen</span>");
                    }
                }
            }
            if (eventName == "closeLogin")
            {
                if(string.IsNullOrEmpty(arguments[0].ToString()) || string.IsNullOrEmpty(arguments[1].ToString()))
                {
                   API.triggerClientEvent(sender, "setLoginmessage", "<span style=\"color: white;\">Bitte f&uuml;lle alle Felder aus</span>");
                } else
                {
                    string query = string.Format("SELECT * FROM User WHERE Name = '{0}' AND Passwort = '{1}'", MySqlHelper.EscapeString(arguments[0].ToString()), Useful.sha256(arguments[1].ToString()));
                    DataTable result = API.exported.database.executeQueryWithResult(query);

                    if (result.Rows.Count == 0)
                    {
                        API.triggerClientEvent(sender, "setLoginmessage", "<span style=\"color: white;\">Das Passwort ist falsch oder diesen Account gibt es nicht</span>");
                    }
                    else
                    {
                        API.triggerClientEvent(sender, "setLoginmessage", "<span style=\"color: green;\">Erfolgreich eingeloggt</span>");
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

                        API.setPlayerName(sender, tmpPlayer.userName);
                        API.setPlayerNametag(sender, tmpPlayer.userName);

                        API.setPlayerSkin(sender, API.pedNameToModel(tmpPlayer.userSkin));

                        API.triggerClientEvent(sender, "hideWindow", "login");
                        API.freezePlayer(sender, false);
                    }
                }
            }
            if (eventName == "showRegister")
            {
                API.triggerClientEvent(sender, "hideWindow", "login");
                API.triggerClientEvent(sender, "showWindow", "register");
            }
            if (eventName == "showLogin")
            {
                API.triggerClientEvent(sender, "hideWindow", "register");
                API.triggerClientEvent(sender, "showWindow", "login");
            }
        }

        public void OnClientConnected(Client player)
        {
            EUS.Players.Add(new Player(player));
            API.freezePlayer(player, true);
        }

        public void OnPlayerFinishedDownload(Client player)
        {
            API.triggerClientEvent(player, "showWindow", "login");
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
