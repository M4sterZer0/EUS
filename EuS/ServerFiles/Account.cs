using GTANetworkServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuS
{
    public class Player
    {
        public Client client { get; set; }

        public int userID { get; set; }
        public string userName { get; set; }
        public string userPasswort { get; set; }
        public int userBank { get; set; }
        public int userCar { get; set; }
        public int userMoney { get; set; }
        public string userSkin { get; set; }
        public int userHouse { get; set; }
        public int userFrakID { get; set; }
        public int userTelNr { get; set; }
        public int userLastLogin { get; set; }
        public int userRegister { get; set; }
        public string userTS3ID { get; set; }
        public int userForumID { get; set; }
        public int userPremium { get; set; }
        public int userPremiumTime { get; set; }
        public int userAdminRank { get; set; }
        public string userRegIP { get; set; }
        public string userLastIP { get; set; }
        public int userBan { get; set; }
        public int userWarns { get; set; }
        public float userLogoutX { get; set; }
        public float userLogoutY { get; set; }
        public float userLogoutZ { get; set; }
        public int userAduty { get; set; }
        public int userFrakrank { get; set; }
        public int userFrakDuty { get; set; }
        public int userWanteds { get; set; }
        public int userJobID { get; set; }
        public int userJobDuty { get; set; }
        public bool userLoggedin { get; set; }

        public Player() { }
        public Player(Client client)
        {
            this.client = client;
            this.userLoggedin = false;
        }

        public static Player getPlayerAccount(Client player)
        {
            foreach (Player p in EUS.Players)
            {
                if (p.client == player)
                {
                    return p;
                }
            }
            return null;
        }
    }
}